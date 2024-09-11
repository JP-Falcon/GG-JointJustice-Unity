&MODE:Dialogue
&SCENE:TMPH_Lobby_With_Details
&ACTOR:Dan
&SPEAK:Dan
    So where do you wanna go next?

&MODE:Investigation
&ADD_FAILURE_SCRIPT:INVESTIGATIONTESTFAIL1
&ADD_FAILURE_SCRIPT:INVESTIGATIONTESTFAIL2

-> Choice

=== Choice ===
&HIDE_TEXTBOX
+ [So where's your Dad? #Talk]
    -> Dad
+ [Does it smell like Updog in here? #Talk]
    -> Updog
+ [So about Rachel #Talk #Locked]
    -> Rachel
+ [Kitchen #Move #TMPHWideShot]
    -> Kitchen
+ [Connecticut #Move #Locked #TMPHProsecution]
    -> Connecticut

=== Dad ===
&SPEAK:Arin
So where's your Dad?
&ACTOR:Dan
&SPEAK:Dan
He's in the kitchen, making a sandwich.
&SET_POSE:Angry
Why do you ask?
&SPEAK:Arin
NO REASON, LET'S MOVE ON!
-> Choice

=== Updog ===
&ACTOR:Dan
&SET_POSE:Normal
&SPEAK:Arin
Does it smell like updog in here?
&SPEAK:Dan
...what's updog?
&SPEAK:Arin
Not much, what's up with you?
&HIDE_TEXTBOX
&ACTOR:Dan
&WAIT:1
&SET_POSE:Angry
&WAIT:1.5
&SPEAK:Dan
This is why we'll never be real detectives, Arin.
&UNLOCK_CHOICE:So about Rachel,Talk 
-> Choice

=== Rachel ===
&ACTOR:Dan
&SET_POSE:Normal
&SPEAK:Arin
So what's up with Rachel?
&SPEAK:Dan
Don't you mean Laura?
I think she moved to Connecticut.
&UNLOCK_CHOICE:Connecticut,Move
-> Choice

=== Kitchen ===
&ACTOR:Dan
&SET_POSE:Normal
&SPEAK:Arin
Sorry kitten, but if we go there tests will break.
&SPEAK:Dan
Arin, what the fuck are you talking about?
&SPEAK:Arin
Don't worry about it, okay?
-> Choice

=== Connecticut ===
&LOAD_SCRIPT:Case1/1-1-Intro
-> END
