&SCENE:TMPH_Court
&SET_ACTOR_POSITION:Defense,Arin
&JUMP_TO_POSITION:Defense
&SET_POSE:Thinking
&THINK:Arin
Hm...
&HIDE_TEXTBOX

+ [Object!]
    -> playerObjects
+ [Nah, we gucci]
    -> playerDoesntObject

=== playerObjects ===
&OBJECTION:Arin
&PLAY_SFX:deskSlam
&PLAY_EMOTION:DeskSlamAnimation
&SPEAK:Arin
My client's behavior during the livestream isn't suspicious in the least!

-> END

=== playerDoesntObject ===
&SPEAK:Arin
Nah, it's fine. Polishing some coins doesn't prove he's guilty.
-> END