&MODE:Dialogue
&SCENE:TMPH_Lobby_With_Details
&ACTOR:Dan
&SPEAK:Dan
    So where do you wanna go next?

&ADD_EVIDENCE:PlumberInvoice
&ADD_EVIDENCE:JorySrsLetter
&ADD_EVIDENCE:Switch

&MODE:Investigation
&ADD_FAILURE_SCRIPT:INVESTIGATIONTESTFAIL1
&ADD_FAILURE_SCRIPT:INVESTIGATIONTESTFAIL2

-> Choice

=== Choice ===
&HIDE_TEXTBOX
+ [Introduction #id:introduction #Talk #Initial]
    -> Initial
+ [So where's your Dad? #id:dad #Talk]
    -> Dad
+ [Does it smell like Updog in here? #id:updog #Talk]
    -> Updog
+ [So about Rachel #id:rachel #Talk #Locked]
    -> Rachel
+ [The Letter #id:letter #Talk #Locked]
    -> LetterTalk
+ [Kitchen #Move #id:kitchen #background:TMPHWideShot]
    -> Kitchen
+ [Connecticut #Move #Locked #id:connecticut #background:TMPHProsecution]
    -> Connecticut
+ [Kitchen But Different #Move #Locked #id:kitchen2 #background:TMPHWideShot]
    -> KitchenDifferent
+ [JorySrsLetter #Evidence]
    -> LetterEvidence
+ [Switch #Evidence]
    -> Switch

=== LetterEvidence ===
&ACTOR:Dan
&SET_POSE:Normal
&SPEAK:Arin
So what's this letter about?
&SPEAK:Dan
Have you read it?
&SPEAK:Arin
No?
&SPEAK:Dan
Then I guess we'll never know, won't we?
&UNLOCK_TALK_CHOICE:letter
->Choice

=== LetterTalk ===
&ACTOR:Dan
&SET_POSE:Normal
&SPEAK:Arin
So back to the letter.
&SPEAK:Dan
Have you read it by now?
&SPEAK:Arin
I thought this is the dialogue where you read it to me.
&SPEAK:Dan
The what now?
&SPEAK:Arin
Nevermind.
->Choice

=== Switch ===
&ACTOR:Dan
&SET_POSE:Normal
&SPEAK:Arin
What's this switch for?

&SPEAK:Dan
Not the lights, that's for sure.

&SPEAK:Arin
Dude. Nice.
&LOCK_TALK_CHOICE:letter
-> Choice

=== Initial ===
&ACTOR:Dan
&SET_POSE:Normal
&SPEAK:Dan
What're we doing today, Arin?
-> Choice

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
&UNLOCK_TALK_CHOICE:updog
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
&UNLOCK_TALK_CHOICE:rachel
-> Choice

=== Rachel ===
&ACTOR:Dan
&SET_POSE:Normal
&SPEAK:Arin
So what's up with Rachel?
&SPEAK:Dan
Don't you mean Laura?
I think she moved to Connecticut.
&UNLOCK_MOVE_CHOICE:connecticut
&LOCK_TALK_CHOICE:updog
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
&UNLOCK_MOVE_CHOICE:kitchen2
-> Choice

=== KitchenDifferent ===
&ACTOR:Dan
&SET_POSE:Normal
&SPEAK:Arin
We still can't go here.
&SPEAK:Dan
&SET_POSE:Angry
Then why are we having this conversation?!
&SPEAK:Arin
To lock a move choice, duh!
&LOCK_MOVE_CHOICE:kitchen2
-> Choice

=== Connecticut ===
&LOAD_SCRIPT:Case1/1-1-Intro
-> END
