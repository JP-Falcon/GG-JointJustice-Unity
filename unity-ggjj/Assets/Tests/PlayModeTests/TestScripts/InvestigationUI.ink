&MODE:Dialogue
&SCENE:TMPH_Lobby
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
+ [So about Rachel #Present]
    -> Rachel
+ [Kitchen #Move]
    -> Rachel
+ [Office #Move #Locked]
    -> Rachel

=== Dad ===
&SHOW_TEXTBOX
&ACTOR:Dan
&SPEAK:Dan
He's in the kitchen, making a sandwich.
Why do you ask?
&AUDIO:Arin
NO REASON LET'S MOVE ON
&UNLOCK_CHOICE:So about Rachel,Talk

=== Updog ===
&SHOW_TEXTBOX
&ACTOR:Arin
&SPEAK:Arin
Does it smell like updog in here?
&SPEAK:Dan
What's updog?
&SPEAK:Arin
Not much, what's up with you?

=== Rachel ===
&SHOW_TEXTBOX
&ACTOR:Arin
&SPEAK:Arin
So what's up with Rachel?
&SPEAK:Dan
Don't you mean Laura?
I think she moved to Connecticut.
&UNLOCK_CHOICE:Office,Move



-> END

