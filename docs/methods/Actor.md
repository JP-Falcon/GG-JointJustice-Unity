## ACTOR

⏲ Instant

Sets the current shown actor on screen to the one provided. Starts it in the normal pose.

Examples: 
  - `&ACTOR:Arin`

## SHOW_ACTOR

⏲ Instant

Shows the current active actor, or a specified actor in the scene

Examples: 
  - `&SHOW_ACTOR`
  - `&SHOW_ACTOR:Arin`

## HIDE_ACTOR

⏲ Instant

Hides the current active actor, or a specified actor in the scene

Examples: 
  - `&HIDE_ACTOR`
  - `&HIDE_ACTOR:Arin`

## SET_POSE
Values: 
  - [Poses defined per Actor](../constants.md#ActorPoseAssetName)

⏲ Instant

Makes the currently shown actor switch to target pose. Plays any animation associated with target pose / emotion, but doesn't wait until it is finished before continuing.

Examples: 
  - `&SET_POSE:Normal`

## PLAY_EMOTION

⏳ Waits for completion

Makes the currently shown actor perform target emotion (fancy word animation on an actor). Practically does the same as SET_POSE, but waits for the emotion to complete. Doesn't work on all poses, possible ones are flagged.

Examples: 
  - `&PLAY_EMOTION:Nodding`

## SET_ACTOR_POSITION

⏲ Instant

Places an actor in the specified slot in the scene

Examples: 
  - `&SET_ACTOR_POSITION:Defense,Arin`