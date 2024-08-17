using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Scanner;

internal static class AssetParser
{
    public static List<PathItem> ConvertToMarkdown(string absolutePathToAssetsDirectory, Dictionary<string, string> pathsByGUID, string relativeFileName)
    {
        var contents = File.ReadAllText(Path.Join(absolutePathToAssetsDirectory, relativeFileName));
        if (contents[0] == '%')
        {
            contents = string.Join("\n", contents.Split('\n').Skip(3));
        }

        var deserializer = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .Build();
        var asset = deserializer.Deserialize<dynamic>(contents)["MonoBehaviour"];
        string type = Path.GetFileNameWithoutExtension(pathsByGUID[asset["m_Script"]["guid"]]);
        switch (type)
        {
            case "EvidenceData":
                var iconField = asset["<Icon>k__BackingField"];
                if (iconField["fileID"] == "0")
                {
                    throw new NotSupportedException($"Evidence '{relativeFileName}' has no icon sprite");
                }

                string relativeIconPath = Path.Join("..", Path.GetFileName(absolutePathToAssetsDirectory), "Assets", pathsByGUID[iconField["guid"]]);
                return [new PathItem()
                {
                    Item = Path.GetFileNameWithoutExtension(relativeFileName),
                    Description = asset["<Description>k__BackingField"],
                    RelativeIconPath = relativeIconPath.Replace(Path.DirectorySeparatorChar, '/')
                }];
            case "ActorData":
                var profileField = asset["<Profile>k__BackingField"];
                if (profileField["fileID"] == "0")
                {
                    throw new NotSupportedException($"Actor '{relativeFileName}' has no profile sprite");
                }

                string relativeProfilePath = Path.Join("..", Path.GetFileName(absolutePathToAssetsDirectory), "Assets", pathsByGUID[profileField["guid"]]);
                return [new PathItem()
                {
                    Item = Path.GetFileNameWithoutExtension(relativeFileName),
                    Description = asset["<Bio>k__BackingField"],
                    RelativeIconPath = relativeProfilePath.Replace(Path.DirectorySeparatorChar, '/')
                }];
        }
        
        return [new PathItem() { Item = Path.GetFileNameWithoutExtension(relativeFileName) }];
    }
}

[DebuggerDisplay("{Item}: {Children}")]
public class PathItem
{
    public string Item { get; init; }
    public string Description { get; init; }
    public string RelativeIconPath { get; init; }
    public bool IsComplex => !string.IsNullOrEmpty(Description) || !string.IsNullOrEmpty(RelativeIconPath);
    public IEnumerable<PathItem> Children { get; init; }

    private static readonly Dictionary<string, Func<string, Dictionary<string, string>, string, List<PathItem>>> filetypeOverrides = new()
    {
        { "controller", AnimationControllerParser.ConvertToMarkdown },
        { "asset", AssetParser.ConvertToMarkdown }
    };
    
    public static List<PathItem> Generate(string absolutePathToAssetsDirectory, Dictionary<string, string> pathsByGUID, string relativeFileName)
    {
        var fileEnding = relativeFileName.Split(".").Last();
        if (filetypeOverrides.TryGetValue(fileEnding, out var creationMethod))
        {
            return creationMethod(absolutePathToAssetsDirectory, pathsByGUID, relativeFileName);
        }
        
        return [new PathItem() { Item = Path.GetFileNameWithoutExtension(relativeFileName) }];

    }
}

public class XMLDocParser
{
    public Dictionary<string, string> ParameterTypesToPath = new();
    public Dictionary<string, List<MethodInfo>> MethodsByCategory = new();

    public void Parse(string methodName, string commentsForMethod, List<ParameterSyntax> parameterList)
    {
        var doc = new XmlDocument();
        doc.LoadXml(commentsForMethod);

        var root = doc.SelectSingleNode("root");

        var category = root.SelectSingleNode("category");
        var isInstant = bool.Parse(root.SelectSingleNode("isInstant").FirstChild.Value);
        if (category == null)
        {
            throw new FormatException("No <category> supplied");
        }

        if (!MethodsByCategory.ContainsKey(category.FirstChild.Value))
        {
            MethodsByCategory.Add(category.FirstChild.Value, new());
        }

        MethodsByCategory[category.FirstChild.Value].Add(new(root, methodName, parameterList, isInstant, ref ParameterTypesToPath));
    }

    public Dictionary<string, List<PathItem>> ResolveAssets(string absolutePathToAssetDirectory, Dictionary<string, string> relativePathsByGUID)
    {
        var dependentPaths = new List<(string, string)>();
        var filesByType = new Dictionary<string, List<PathItem>>();
        foreach (var (parameterType, parameterValuesPaths) in ParameterTypesToPath)
        {
            var paths = parameterValuesPaths.Split(',');
            filesByType[parameterType] = new List<PathItem>();
            foreach(var parameterValuesPath in paths)
            {
                if (parameterValuesPath.Contains("{"))
                {
                    dependentPaths.Add((parameterType, parameterValuesPath));
                    continue;
                }

                var folderPath = Path.GetDirectoryName(parameterValuesPath);
                var fileMask = Path.GetFileName(parameterValuesPath);
                SearchOption searchOption = SearchOption.TopDirectoryOnly;
                if (fileMask.Contains("**")) {
                    searchOption = SearchOption.AllDirectories;
                    fileMask.Replace("**", "*");
                }
                filesByType[parameterType].AddRange(Directory.EnumerateFiles(folderPath, fileMask, searchOption)
                    .ToList()
                    .SelectMany(path=>PathItem.Generate(absolutePathToAssetDirectory, relativePathsByGUID, path)));
            }
        }

        var subPathRegex = new Regex(@"\{([^{]*)\}");
        foreach (var (dependentType, dependentPath) in dependentPaths)
        {
            if (!filesByType.ContainsKey(dependentType))
            {
                filesByType[dependentType] = new List<PathItem>();
            }
            var key = subPathRegex.Match(dependentPath).Groups[1].Captures[0].Value;
            foreach (var subItem in filesByType[key])
            {
                var subFolder = dependentPath.Replace($"{{{key}}}", Path.GetFileNameWithoutExtension(subItem.Item));
                var folderPath = Path.GetDirectoryName(subFolder)!;
                var fileMask = Path.GetFileName(subFolder)!;
                var folder = folderPath.Split(Path.DirectorySeparatorChar).Last();

                var matchingFiles = new List<PathItem>();
                SearchOption searchOption = SearchOption.TopDirectoryOnly;
                if (fileMask.Contains("**")) {
                    searchOption = SearchOption.AllDirectories;
                    fileMask.Replace("**", "*");
                }

                foreach (var absoluteFilePath in Directory.EnumerateFiles(folderPath, fileMask, searchOption))
                {
                    matchingFiles.AddRange(PathItem.Generate(absolutePathToAssetDirectory, relativePathsByGUID, absoluteFilePath));
                }

                filesByType[dependentType] = filesByType[dependentType].Concat(new List<PathItem>(){new() { Children = matchingFiles, Item = folder } }).ToList();
            }

            filesByType[dependentType] = filesByType[dependentType].ToList();
        }

        return filesByType;
    }
}