#define OnUnreadableTexture_ReadFileDirectly
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor.Scripts
{
    public struct PixelSolidCondition
    {
        public static readonly PixelSolidCondition Default = new(ChannelType.Alpha, ConditionType.GreaterThan, 0.5f);

        public enum ChannelType
        {
            Alpha = 0,
            Brightness = 1,
            Red = 2,
            Green = 3,
            Blue = 4
        }
        public ChannelType Channel;
        public enum ConditionType
        {
            GreaterThan = 0,
            LessThan = 1
        }
        public ConditionType Condition;
        public float Threshold;

        private PixelSolidCondition(ChannelType channelType, ConditionType conditionType, float threshold)
        {
            Channel = channelType;
            Condition = conditionType;
            Threshold = Mathf.Clamp01(threshold);
        }

        public readonly bool IsPixelSolid(Color pixel)
        {
            var value = Channel switch
            {
                ChannelType.Brightness => (pixel.r + pixel.g + pixel.b) / 3.0f,
                ChannelType.Red => pixel.r,
                ChannelType.Green => pixel.g,
                ChannelType.Blue => pixel.b,
                _ => pixel.a
            };

            if (Condition is ConditionType.LessThan)
            {
                return value < Threshold;
            }

            return value > Threshold;
        }
    }

    public struct LineSegment
    {
        public Vector2Int Start;
        public Vector2Int End;
    }

    public static class PixelTracingHelper
    {
        public static void TraceAndApplyPhysicsShape(Texture2D texture, PixelSolidCondition pixelSolidCondition)
        {
            var textureAssetPath = AssetDatabase.GetAssetPath(texture);
            var textureImporter = AssetImporter.GetAtPath(textureAssetPath) as TextureImporter;
            var spriteDataProviderFactory = new SpriteDataProviderFactories();
            spriteDataProviderFactory.Init();
            var spriteEditorDataProvider = spriteDataProviderFactory.GetSpriteEditorDataProviderFromObject(textureImporter);
            spriteEditorDataProvider.InitSpriteEditorDataProvider();
            var physicsOutlineDataProvider = spriteEditorDataProvider.GetDataProvider<ISpritePhysicsOutlineDataProvider>();

            var spriteRects = spriteEditorDataProvider.GetSpriteRects();
            foreach (var spriteRect in spriteRects)
            {
                var pixelRect = new RectInt((int)spriteRect.rect.x, (int)spriteRect.rect.y, (int)spriteRect.rect.width, (int)spriteRect.rect.height);
                var pixelPolygons = TraceTexture(texture, pixelSolidCondition, pixelRect);
                var polygons = new Vector2[pixelPolygons.Length][];
                var offsetX = -(pixelRect.width / 2.0f);
                var offsetY = -(pixelRect.height / 2.0f);
                for (var j = 0; j < pixelPolygons.Length; j++)
                {
                    var pixelPolygon = pixelPolygons[j];
                    var polygon = new Vector2[pixelPolygon.Length];
                    for (var k = 0; k < pixelPolygon.Length; k++)
                    {
                        polygon[k] = new(pixelPolygon[k].x + offsetX, pixelPolygon[k].y + offsetY);
                    }
                    polygons[j] = polygon;
                }
                physicsOutlineDataProvider.SetOutlines(spriteRect.spriteID, new(polygons));
            }

            spriteEditorDataProvider.Apply();
            EditorUtility.SetDirty(textureImporter);
            textureImporter!.SaveAndReimport();
            AssetDatabase.ImportAsset(textureAssetPath, ImportAssetOptions.ForceUpdate);
        }

        public static Vector2[][] TraceSprite(Sprite sprite, PixelSolidCondition pixelSolidCondition)
        {
            if (sprite == null)
            {
                return Array.Empty<Vector2[]>();
            }

            var pixelPolygons = TraceTexture(sprite.texture, pixelSolidCondition, new RectInt((int)sprite.rect.xMin, (int)sprite.rect.yMin, (int)sprite.rect.width, (int)sprite.rect.height));

            var scale = 1.0f / sprite.pixelsPerUnit;
            var offsetX = -(sprite.pivot.x * scale);
            var offsetY = -(sprite.pivot.y * scale);
            var polygons = new Vector2[pixelPolygons.Length][];
            for (var i = 0; i < pixelPolygons.Length; i++)
            {
                var pixelPolygon = pixelPolygons[i];
                var polygon = new Vector2[pixelPolygon.Length];
                for (var j = 0; j < pixelPolygon.Length; j++)
                {
                    polygon[j] = new((pixelPolygon[j].x * scale) + offsetX, (pixelPolygon[j].y * scale) + offsetY);
                }
                polygons[i] = polygon;
            }
            return polygons;
        }

        private static Vector2Int[][] TraceTexture(Texture2D texture, PixelSolidCondition pixelSolidCondition, RectInt? rect = null)
        {
            if (texture == null || texture.width == 0 || texture.height == 0)
            {
                return Array.Empty<Vector2Int[]>();
            }
            rect ??= new(0, 0, texture.width, texture.height);
            if (rect.Value.width == 0 || rect.Value.height == 0)
            {
                throw new("Rect cannot have a width or height of 0.");
            }
            if (rect.Value.xMin > 0 || rect.Value.yMin > 0 || rect.Value.xMax > texture.width || rect.Value.yMax > texture.height)
            {
                throw new("Rect must be contained within the bounds of texture.");
            }
            if (!texture.isReadable)
            {
                texture = HandleUnreadableTexture(texture);
            }
            return TraceTextureInternal(texture, pixelSolidCondition, (RectInt)rect);
        }

        private static Texture2D HandleUnreadableTexture(Texture2D texture)
        {
            var textureAssetPath = AssetDatabase.GetAssetPath(texture);
            if (!File.Exists(textureAssetPath))
            {
                throw new("Texture does not have an associated asset file and therefore could not be read directly.");
            }
            if (EditorApplication.isPlaying)
            {
                Debug.LogWarning("Texture was read directly from the asset file during runtime. This only works in debug builds and is considered unstable.");
            }
            var rawTextureBytes = File.ReadAllBytes(textureAssetPath);
            var loadedTexture = new Texture2D(1, 1);
            loadedTexture.LoadImage(rawTextureBytes);
            return loadedTexture;
        }

        private static Vector2Int[][] TraceTextureInternal(Texture2D texture, PixelSolidCondition pixelSolidCondition, RectInt rect)
        {
            Color[] pixelData;
            if (rect is { x: 0, y: 0 } && rect.width == texture.width && rect.height == texture.height)
            {
                pixelData = texture.GetPixels();
            }
            else
            {
                pixelData = texture.GetPixels(rect.x, rect.y, rect.width, rect.height);
            }

            var solidityMap = new bool[pixelData.Length];
            for (var i = 0; i < pixelData.Length; i++)
            {
                solidityMap[i] = pixelSolidCondition.IsPixelSolid(pixelData[i]);
            }

            var width = rect.width;
            var height = rect.height;

            var currentLineSegmentNull = true;
            var currentLineSegment = new LineSegment();
            var rightLineSegments = new LinkedList<LineSegment>();
            var leftLineSegments = new LinkedList<LineSegment>();
            var upLineSegments = new LinkedList<LineSegment>();
            var downLineSegments = new LinkedList<LineSegment>();

            currentLineSegment.Start.y = 0;
            currentLineSegment.End.y = 0;
            for (var x = 0; x < width; x++)
            {
                if (solidityMap[x]) // (x, 0)
                {
                    if (currentLineSegmentNull)
                    {
                        currentLineSegment.Start.x = x + 1;
                        currentLineSegment.End.x = x;
                        currentLineSegmentNull = false;
                    }
                    else
                    {
                        currentLineSegment.Start.x++;
                    }
                }
                else if (!currentLineSegmentNull)
                {
                    leftLineSegments.AddLast(currentLineSegment);
                    currentLineSegmentNull = true;
                }
            }
            if (!currentLineSegmentNull)
            {
                leftLineSegments.AddLast(currentLineSegment);
            }

            var currentLineSegmentRight = false;
            for (var y = 1; y < height; y++)
            {
                currentLineSegmentNull = true;
                currentLineSegment.Start.y = y;
                currentLineSegment.End.y = y;
                for (var x = 0; x < width; x++)
                {
                    switch (solidityMap[(y * width) + x])
                    {
                        // !(x, y) (x, y - 1)
                        case false when solidityMap[((y - 1) * width) + x]:
                        {
                            if (currentLineSegmentNull || !currentLineSegmentRight)
                            {
                                if (!currentLineSegmentNull)
                                {
                                    leftLineSegments.AddLast(currentLineSegment);
                                }
                                currentLineSegment.Start.x = x;
                                currentLineSegment.End.x = x + 1;
                                currentLineSegmentNull = false;
                                currentLineSegmentRight = true;
                            }
                            else
                            {
                                currentLineSegment.End.x++;
                            }

                            break;
                        }
                        // (x, y) !(x, y - 1)
                        case true when !solidityMap[((y - 1) * width) + x]:
                        {
                            if (currentLineSegmentNull || currentLineSegmentRight)
                            {
                                if (!currentLineSegmentNull)
                                {
                                    rightLineSegments.AddLast(currentLineSegment);
                                }
                                currentLineSegment.Start.x = x + 1;
                                currentLineSegment.End.x = x;
                                currentLineSegmentNull = false;
                                currentLineSegmentRight = false;
                            }
                            else
                            {
                                currentLineSegment.Start.x++;
                            }

                            break;
                        }
                        default:
                        {
                            if (!currentLineSegmentNull)
                            {
                                if (currentLineSegmentRight)
                                {
                                    rightLineSegments.AddLast(currentLineSegment);
                                }
                                else
                                {
                                    leftLineSegments.AddLast(currentLineSegment);
                                }
                                currentLineSegmentNull = true;
                            }

                            break;
                        }
                    }
                }

                if (currentLineSegmentNull) continue;
                if (currentLineSegmentRight)
                {
                    rightLineSegments.AddLast(currentLineSegment);
                }
                else
                {
                    leftLineSegments.AddLast(currentLineSegment);
                }
            }

            currentLineSegmentNull = true;
            currentLineSegment.Start.y = height;
            currentLineSegment.End.y = height;
            for (var x = 0; x < width; x++)
            {
                if (solidityMap[((height - 1) * width) + x]) // (x, height - 1)
                {
                    if (currentLineSegmentNull)
                    {
                        currentLineSegment.Start.x = x;
                        currentLineSegment.End.x = x + 1;
                        currentLineSegmentNull = false;
                    }
                    else
                    {
                        currentLineSegment.End.x++;
                    }
                }
                else if (!currentLineSegmentNull)
                {
                    rightLineSegments.AddLast(currentLineSegment);
                    currentLineSegmentNull = true;
                }
            }
            if (!currentLineSegmentNull)
            {
                rightLineSegments.AddLast(currentLineSegment);
            }

            currentLineSegmentNull = true;
            currentLineSegment.Start.x = 0;
            currentLineSegment.End.x = 0;
            for (var y = 0; y < height; y++)
            {
                if (solidityMap[y * width]) // (0, y)
                {
                    if (currentLineSegmentNull)
                    {
                        currentLineSegment.Start.y = y;
                        currentLineSegment.End.y = y + 1;
                        currentLineSegmentNull = false;
                    }
                    else
                    {
                        currentLineSegment.End.y++;
                    }
                }
                else if (!currentLineSegmentNull)
                {
                    upLineSegments.AddLast(currentLineSegment);
                    currentLineSegmentNull = true;
                }
            }
            if (!currentLineSegmentNull)
            {
                upLineSegments.AddLast(currentLineSegment);
            }

            var currentLineSegmentUp = false;
            for (var x = 1; x < width; x++)
            {
                currentLineSegmentNull = true;
                currentLineSegment.Start.x = x;
                currentLineSegment.End.x = x;
                for (var y = 0; y < width; y++)
                {
                    if (solidityMap.Length >= (y * width) + (x) && solidityMap[(y * width) + x] && !solidityMap[(y * width) + (x - 1)]) // (x, y) !(x - 1, y)
                    {
                        if (currentLineSegmentNull || !currentLineSegmentUp)
                        {
                            if (!currentLineSegmentNull)
                            {
                                downLineSegments.AddLast(currentLineSegment);
                            }
                            currentLineSegment.Start.y = y;
                            currentLineSegment.End.y = y + 1;
                            currentLineSegmentNull = false;
                            currentLineSegmentUp = true;
                        }
                        else
                        {
                            currentLineSegment.End.y++;
                        }
                    }
                    else if (solidityMap.Length >= (y * width) + (x) && !solidityMap[(y * width) + x] && solidityMap[(y * width) + (x - 1)]) // !(x, y) (x - 1, y)
                    {
                        if (currentLineSegmentNull || currentLineSegmentUp)
                        {
                            if (!currentLineSegmentNull)
                            {
                                upLineSegments.AddLast(currentLineSegment);
                            }
                            currentLineSegment.Start.y = y + 1;
                            currentLineSegment.End.y = y;
                            currentLineSegmentNull = false;
                            currentLineSegmentUp = false;
                        }
                        else
                        {
                            currentLineSegment.Start.y++;
                        }
                    }
                    else if (!currentLineSegmentNull)
                    {
                        if (currentLineSegmentUp)
                        {
                            upLineSegments.AddLast(currentLineSegment);
                        }
                        else
                        {
                            downLineSegments.AddLast(currentLineSegment);
                        }
                        currentLineSegmentNull = true;
                    }
                }

                if (currentLineSegmentNull) continue;
                if (currentLineSegmentUp)
                {
                    upLineSegments.AddLast(currentLineSegment);
                }
                else
                {
                    downLineSegments.AddLast(currentLineSegment);
                }
            }

            currentLineSegmentNull = true;
            currentLineSegment.Start.x = width;
            currentLineSegment.End.x = width;
            for (var y = 0; y < height; y++)
            {
                if (solidityMap[(y * width) + (width - 1)]) // (width - 1, y)
                {
                    if (currentLineSegmentNull)
                    {
                        currentLineSegment.Start.y = y + 1;
                        currentLineSegment.End.y = y;
                        currentLineSegmentNull = false;
                    }
                    else
                    {
                        currentLineSegment.Start.y++;
                    }
                }
                else if (!currentLineSegmentNull)
                {
                    downLineSegments.AddLast(currentLineSegment);
                    currentLineSegmentNull = true;
                }
            }
            if (!currentLineSegmentNull)
            {
                downLineSegments.AddLast(currentLineSegment);
            }

            var polygons = new LinkedList<Vector2Int[]>();
            while (leftLineSegments.Count + rightLineSegments.Count > 0)
            {
                var currentPolygon = new LinkedList<Vector2Int>();
                currentPolygon.AddFirst(rightLineSegments.First.Value.Start);
                currentPolygon.AddLast(rightLineSegments.First.Value.End);
                rightLineSegments.RemoveFirst();

                bool currentPolygonGoofy;
                if (AddLineSegment(currentPolygon, downLineSegments))
                {
                    currentPolygonGoofy = false;
                }
                else
                {
                    AddLineSegment(currentPolygon, upLineSegments);
                    currentPolygonGoofy = true;
                }

                while (currentPolygon.First.Value != currentPolygon.Last.Value)
                {
                    if (currentPolygonGoofy)
                    {
                        if (AddLineSegment(currentPolygon, rightLineSegments))
                        {
                            currentPolygonGoofy = false;
                        }
                        else
                        {
                            AddLineSegment(currentPolygon, leftLineSegments);
                        }
                    }
                    else
                    {
                        if (AddLineSegment(currentPolygon, leftLineSegments))
                        {
                            currentPolygonGoofy = true;
                        }
                        else
                        {
                            AddLineSegment(currentPolygon, rightLineSegments);
                        }
                    }
                    if (currentPolygonGoofy)
                    {
                        if (!AddLineSegment(currentPolygon, upLineSegments))
                        {
                            AddLineSegment(currentPolygon, downLineSegments);
                            currentPolygonGoofy = false;
                        }
                    }
                    else
                    {
                        if (!AddLineSegment(currentPolygon, downLineSegments))
                        {
                            AddLineSegment(currentPolygon, upLineSegments);
                            currentPolygonGoofy = true;
                        }
                    }
                }

                currentPolygon.RemoveLast();
                polygons.AddLast(currentPolygon.ToArray());
            }

            return polygons.ToArray();
        }

        private static bool AddLineSegment(LinkedList<Vector2Int> partialPolygon, LinkedList<LineSegment> lineSegments)
        {
            var lastPointInPolygon = partialPolygon.Last.Value;
            var currentNode = lineSegments.First;
            for (var i = 0; i < lineSegments.Count; i++)
            {
                var currentLineSegment = currentNode!.Value;
                if (currentLineSegment.Start == lastPointInPolygon)
                {
                    partialPolygon.AddLast(currentLineSegment.End);
                    lineSegments.Remove(currentNode);
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }

        private static T[] ToArray<T>(this LinkedList<T> list)
        {
            var output = new T[list.Count];
            var currentNode = list.First;
            for (var i = 0; i < list.Count; i++)
            {
                output[i] = currentNode!.Value;
                currentNode = currentNode.Next;
            }
            return output;
        }
    }

    public class PixelPhysicsShapeEditor : EditorWindow
    {
        private struct SelectedTextureInfo
        {
            public readonly Texture2D Texture;
            public readonly string AssetPath;
            public readonly GUID AssetGUID;

            public SelectedTextureInfo(Texture2D texture, string assetPath, GUID assetGUID)
            {
                AssetPath = assetPath;
                Texture = texture;
                AssetGUID = assetGUID;
            }
        }
        private readonly LinkedList<SelectedTextureInfo> _selectedTextures = new();
        private PixelSolidCondition _pixelSolidCondition = PixelSolidCondition.Default;
        private Vector2 _scrollPosition = Vector2.zero;

        [UnityEditor.MenuItem("Window/Pixel Physics Shape Editor")]
        public static void ShowWindow()
        {
            var window = GetWindow<PixelPhysicsShapeEditor>("Pixel Physics Shape Editor");
            window.Show();
        }

        private void OnGUI()
        {
            try
            {
                switch (Event.current.type)
                {
                    case EventType.MouseDown:
                    case EventType.MouseUp:
                    case EventType.MouseMove:
                    case EventType.MouseDrag:
                    case EventType.KeyDown:
                    case EventType.KeyUp:
                    case EventType.ScrollWheel:
                    case EventType.Repaint:
                    case EventType.Layout:
                    case EventType.ContextClick:
                    case EventType.MouseEnterWindow:
                    case EventType.MouseLeaveWindow:
                    case EventType.TouchDown:
                    case EventType.TouchUp:
                    case EventType.TouchMove:
                    case EventType.TouchEnter:
                    case EventType.TouchLeave:
                    case EventType.TouchStationary:
                        UpdateGUI();
                        break;
                    case EventType.DragUpdated:
                    case EventType.DragPerform:
                    case EventType.DragExited:
                        UpdateDragAndDrop();
                        break;
                    case EventType.ValidateCommand:
                    case EventType.ExecuteCommand:
                    case EventType.Ignore:
                    case EventType.Used:
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                Debug.LogWarning("Pixel physics shape editor window has crashed.");
                Close();
            }
        }

        private void UpdateGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Width(position.width), GUILayout.Height(position.height));

            _pixelSolidCondition = GUILayoutHelper.PixelSolidConditionSelector(_pixelSolidCondition, this, "pixelSolidCondition");
            GUILayout.Label("", GUILayout.ExpandWidth(false), GUILayout.MaxWidth(0.0f));

            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
            GUILayout.Label($"Selected Textures ({_selectedTextures.Count}):", GUILayout.ExpandWidth(true));
            if (GUILayout.Button("Clear All", GUILayout.ExpandWidth(false)))
            {
                _selectedTextures.Clear();
            }
            GUILayout.EndHorizontal();

            {
                var currentNode = _selectedTextures.First;
                for (var i = 0; i < _selectedTextures.Count; i++)
                {
                    var selectedTexture = currentNode!.Value;
                    GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
                    GUILayout.Label($"{selectedTexture.Texture.name} ({selectedTexture.AssetPath})", GUILayout.ExpandWidth(true));
                    if (GUILayout.Button(" Remove ", GUILayout.ExpandWidth(false)))
                    {
                        _selectedTextures.Remove(currentNode);
                    }
                    GUILayout.EndHorizontal();
                    currentNode = currentNode.Next;
                }
            }

            if (!GUILayout.Button("Generate And Apply Physics Shapes"))
            {
                EditorGUILayout.EndScrollView();
                return;
            }
        
            EditorGUILayout.EndScrollView();

            if (_selectedTextures.Count == 0)
            {
                EditorUtility.DisplayDialog("No textures selected", "In order to generate and apply pixel perfect physics shapes you first need to select some textures. To select textures drag and drop them onto this window.", "OK");
                return;
            }

            var confirm = EditorUtility.DisplayDialog(
                "Are you sure?",
                $"Are you sure you want to change the physics shape of {_selectedTextures.Count} {(_selectedTextures.Count == 1 ? "texture?" : "textures?")}",
                "Yes",
                "No (cancel and go back)"
            );

            if (!confirm)
            {
                return;
            }
        
            var node = _selectedTextures.First;
            for (var i = 0; i < _selectedTextures.Count; i++)
            {
                var selectedTexture = node!.Value;
                PixelTracingHelper.TraceAndApplyPhysicsShape(selectedTexture.Texture, _pixelSolidCondition);
                node = node.Next;
            }
            _selectedTextures.Clear();

        }

        private void UpdateDragAndDrop()
        {
            switch (Event.current.type)
            {
                case EventType.DragUpdated:
                    DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
                    break;
                case EventType.DragExited:
                    DragAndDrop.visualMode = DragAndDropVisualMode.None;
                    break;
                case EventType.DragPerform:
                    DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
                    DragAndDrop.AcceptDrag();
                    var droppedObjects = DragAndDrop.objectReferences;
                    foreach (var obj in droppedObjects)
                    {
                        OnDrop(obj);
                    }
                    break;
            }
        }

        private void OnDrop(Object droppedObject)
        {
            switch (droppedObject)
            {
                case Texture2D texture2D:
                {
                    var droppedTexture = texture2D;
                    OnDropTexture(droppedTexture);
                    break;
                }
                case DefaultAsset asset:
                {
                    var droppedAsset = asset;
                    OnDropFolder(droppedAsset);
                    break;
                }
            }
        }
        private void OnDropTexture(Texture2D droppedTexture)
        {
            if (!droppedTexture)
            {
                return;
            }
            var droppedAssetPath = AssetDatabase.GetAssetPath(droppedTexture);
            var droppedAssetGUID = AssetDatabase.GUIDFromAssetPath(droppedAssetPath);
            {
                var currentNode = _selectedTextures.First;
                for (var i = 0; i < _selectedTextures.Count; i++)
                {
                    if (currentNode != null && currentNode.Value.AssetGUID == droppedAssetGUID)
                    {
                        return;
                    }

                    currentNode = currentNode?.Next;
                }
            }
            _selectedTextures.AddFirst(new SelectedTextureInfo(droppedTexture, droppedAssetPath, droppedAssetGUID));
        }
        private void OnDropFolder(DefaultAsset droppedFolder)
        {
            var droppedFolderPath = AssetDatabase.GetAssetPath(droppedFolder);
            if (!Directory.Exists(droppedFolderPath))
            {
                return;
            }
            var droppedFiles = Directory.GetFiles(droppedFolderPath, "*", SearchOption.AllDirectories);
            foreach (var droppedFile in droppedFiles)
            {
                OnDropFile(droppedFile);
            }
        }
        private void OnDropFile(string droppedFile)
        {
            var ext = Path.GetExtension(droppedFile).ToLower();
            if (ext != ".png" && ext != ".jpg" && ext != ".jpeg")
            {
                return;
            }
        
            var droppedTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(droppedFile);
            if (droppedTexture)
            {
                OnDropTexture(droppedTexture);
            }
        }
    }

    public static class GUILayoutHelper
    {
        public static PixelSolidCondition PixelSolidConditionSelector(PixelSolidCondition value, UnityEditor.Editor context, string selectorID)
        {
            // 18.0f is the size of the padding in DIPs between the edge of the inspector window and the actual content area.
            return PixelSolidConditionSelectorInternal(value, context, EditorGUIUtility.currentViewWidth - 18.0f, selectorID);
        }
        public static PixelSolidCondition PixelSolidConditionSelector(PixelSolidCondition value, EditorWindow context, string selectorID)
        {
            return PixelSolidConditionSelectorInternal(value, context, EditorGUIUtility.currentViewWidth, selectorID);
        }
        private static PixelSolidCondition PixelSolidConditionSelectorInternal(PixelSolidCondition value, object context, float contextWidth, string selectorID)
        {
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
            GUILayout.Label("Consider pixel solid if their", GUILayout.ExpandWidth(false));
            if (contextWidth <= 481.0f)
            {
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
            }
            value.Channel = EnumDropdownInternal(value.Channel, context, selectorID + ".Channel");
            GUILayout.Label("is", GUILayout.ExpandWidth(false));
            value.Condition = EnumDropdownInternal(value.Condition, context, selectorID + ".Condition");
            value.Threshold = Mathf.Clamp01(EditorGUILayout.DelayedFloatField(value.Threshold, GUILayout.ExpandWidth(false), GUILayout.MaxWidth(90.0f)));
            GUILayout.EndHorizontal();
            return value;
        }

        private struct DropdownUpdatePacket
        {
            public readonly object NewValue;
            public readonly object Context;
            public readonly string DropdownID;
            public DropdownUpdatePacket(object newValue, object context, string dropdownID)
            {
                NewValue = newValue;
                Context = context;
                DropdownID = dropdownID;
            }
        }
        private static readonly LinkedList<DropdownUpdatePacket> UpdatedDropdowns = new();

        private static T EnumDropdownInternal<T>(T value, object context, string dropdownID) where T : struct, Enum
        {
            var values = (T[])Enum.GetValues(typeof(T));
            var names = new string[values.Length];
            for (var i = 0; i < values.Length; i++)
            {
                names[i] = FormatName(values[i].ToString());
            }
            return DropdownInternal(value, FormatName(value.ToString()), values, names, context, dropdownID);
        }

        private static T DropdownInternal<T>(T value, string valueName, T[] values, string[] names, object context, string dropdownID)
        {
            if (EditorGUILayout.DropdownButton(new(valueName), FocusType.Keyboard, GUILayout.ExpandWidth(false)))
            {
                var menu = new GenericMenu();
                for (var i = 0; i < values.Length; i++)
                {
                    var currentValue = values[i];
                    menu.AddItem(new(names[i]), value.Equals(currentValue), () =>
                    {
                        lock (UpdatedDropdowns)
                        {
                            UpdatedDropdowns.AddFirst(new DropdownUpdatePacket(currentValue, context, dropdownID));
                        }
                        context.GetType().GetMethod("Repaint")?.Invoke(context, null);
                    });
                }
                menu.ShowAsContext();
            }
            lock (UpdatedDropdowns)
            {
                var currentNode = UpdatedDropdowns.First;
                for (var i = 0; i < UpdatedDropdowns.Count; i++)
                {
                    if (currentNode == null)
                    {
                        continue;
                    }

                    var updatedDropdown = currentNode.Value;
                    if (updatedDropdown.Context == context && updatedDropdown.DropdownID == dropdownID)
                    {
                        value = (T)updatedDropdown.NewValue;
                        UpdatedDropdowns.Remove(currentNode);
                        break;
                    }
                    currentNode = currentNode.Next;
                }
            }
            return value;
        }

        private static readonly bool LowercaseEnumNames = true;
        private static readonly bool SpaceSplitEnumNames = true;
        private static string FormatName(string name)
        {
            var stringBuilder = new StringBuilder(name.Length);
            foreach (var c in name)
            {
                if (char.IsUpper(c))
                {
                    if (stringBuilder.Length > 0 && SpaceSplitEnumNames)
                    {
                        stringBuilder.Append(' ');
                    }

                    stringBuilder.Append(LowercaseEnumNames ? char.ToLower(c) : c);
                }
                else
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString();
        }
    }
}