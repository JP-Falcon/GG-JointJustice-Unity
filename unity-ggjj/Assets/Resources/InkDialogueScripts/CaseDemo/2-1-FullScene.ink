//Blue = #2064e3
//Red = #d10a1e
//Green = #5ee320


//---------------------------------------------------[Opening Scene]---------------------------------------------------//
//TODO:Add typewriter time, date, location, etc. scenes to important area transitions

&FADE_OUT:0
&HIDE_TEXTBOX
&DIALOGUE_SPEED:0.03
&PUNCTUATION_SPEED:0.2
&SCENE:TMPH_Lobby
&ACTOR:Laura
&SET_POSE:Sweaty
&PLAY_SONG:PrologueInPMinor,4
&FADE_IN:6
&WAIT:0.5
&SPEAK:Laura
	Oh my GAAAAD… Those chicken nuggets aren't yooooours!
	
&SHAKE_SCREEN:0.25,0.2
&PLAY_SFX:stab
&SPEAK_UNKNOWN:Tutorial_Boy
&ACTOR:Tutorial_Boy
	...
	
&ACTOR:Laura
&SPEAK:Laura
&SET_POSE:Sweaty
	I-I gotta report thiiis… it's my jooooob…
	
&SPEAK_UNKNOWN:Tutorial_Boy
&ACTOR:Tutorial_Boy
	You won't tell a soul about this…
&SET_POSE:Angry
	Because otherwise…
	I'll tell EVERYBODY about what YOU did…!
	
&PLAY_SFX:RossEvilLaugh
&SET_POSE:Confident
&WAIT:0.7
&FADE_OUT_SONG:2
&FADE_OUT:2
&HIDE_TEXTBOX

//---------------------------------------[Game Grumps Office]------------------------------------------//
&SCENE:Office
&HIDE_ACTOR
&ACTOR:Arin
&WAIT:1.5
&PLAY_SONG:turnaboutGrumpsters,2.5
&FADE_IN:2

&THINK:Arin
&SET_POSE:Thinking
	<color=\#2064e3>(Wow, what a day of laywerin'. Who could have guessed that Thomas the 18th Century Boy was killed by his own father?)
&THINK:Arin
	<color=\#2064e3>(I never could have guessed you had to go to court for things other than dino related crimes.)
&PLAY_ANIMATION:Blink
&PLAY_SFX:lightbulb
&WAIT:0.1
&SCENE:Office
&THINK:Arin
&SET_POSE:normal
	<color=\#2064e3>(All this work has got me starved. Luckily it's almost 4<link=character>:</link>30 already!)
&THINK:Arin
&SET_POSE:Confident
	<color=\#2064e3>(Time go home, load up on carbohydrates, and lose another Magic game!)
	
&SHAKE_SCREEN:0.55,0.5
&PLAY_SFX:damage2
&STOP_SONG
&SPEAK_UNKNOWN:Dan
&SET_POSE:ShockAnimation
	ARIN!! IT'S ALL GONE TO SHIT!!
	
&SPEAK:Arin
	Uh... What?

&SHOW_ACTOR
&ACTOR:Dan
&SPEAK:Dan
&SET_POSE:Angry
&PLAY_SFX:shock2
&PLAY_SONG:Prelude6969,1
&SHAKE_SCREEN:0.4,0.4
	It's all gone to shit! There's <color=\#d10a1e>a goddamn thief</color> in the building!
	
&PLAY_ANIMATION:Blink
&SCENE:Office
&SHAKE_SCREEN:0.25,0.25
&PLAY_SFX:stab2
&THINK:Arin
	A thief? Did we get robbed?!
	
&SPEAK:Dan
&SET_POSE:Angry
&PLAY_SFX:Smack
&SHAKE_SCREEN:0.25,0.4
//TODO:Change to italics "I" once able
	<color=\#d10a1e>I got robbed</color>, Arin! I was cooking up some snacks in the kitchen and stepped out just for a minute…
&SET_POSE:SideNormal
&PLAY_ANIMATION:Blink
&SCENE:Office
	And when I came back, 
&PLAY_SFX:realization
&CONTINUE_DIALOGUE
	<color=\#d10a1e>my dino nuggies were stolen!
	
&PLAY_ANIMATION:Blink
&SCENE:Office
&THINK:Arin
&PLAY_SFX:realization
	<color=\#d10a1e>Your dino nuggies were stolen?!
	
&SPEAK:Dan
&SET_POSE:Angry
	Yeah,
&PLAY_SFX:realization
&CONTINUE_DIALOGUE
	<color=\#d10a1e>my dino nuggies were stolen</color>.
	Hear the plot point bells going off?
	
&THINK:Arin
	Alright, alright, don't get meta this early in the game. Just tell me exactly what happened.
	
&SHAKE_SCREEN:0.25,0.25
&PLAY_SFX:damage1
&SPEAK:Dan
	It's as I said, man: I was hungry, so I decided to heat up some of those frozen <color=\#d10a1e>dinosaur-shaped chicken nuggets</color> we have in the freezer.
&SET_POSE:Normal
	It has to heat up for a while, and then you have to let it cool for even longer, so I decided to leave them to cook for about <color=\#d10a1e>ten minutes</color>.

&THINK:Arin
	Seriously? That long? For frozen chicken?
	
&SPEAK:Dan
	This is chicken on another level, Arin. First time it burned me HARD! 
&SET_POSE:AirGuitar
	I'm talking hotter than the new NSP album.
	
&THINK:Arin
	Ha, nice plug.
	
&SET_POSE:Normal
&SPEAK:Dan
	Anyway, I came back in and they were totally gone! The plate was still in the microwave, the cutlery was still there… but the nuggets had vanished!

&THINK:Arin
	Did you, uh… look properly?
	
&SHAKE_SCREEN:0.25,0.3
&PLAY_SFX:stab2
&SPEAK:Dan
&SET_POSE:Angry
	Of course I looked properly! Food doesn't just grow legs and walk off, Arin!
	
&PLAY_ANIMATION:Blink
&SCENE:Office
&THINK:Arin
&PLAY_SFX:lightbulb
	Uh, Burgie did? And don't dino nuggies have legs already?
	
&SET_POSE:Lean
&WAIT:1.5
&SPEAK:Dan
&SET_POSE:Angry
	…Okay. Yeah. You got me there. Very true.
&SHAKE_SCREEN:0.25,0.4
&PLAY_SFX:smack
&SET_POSE:Normal
	But these dino nuggies weren't showing ANY signs of becoming sentient! There were crumbs on the plate; <color=\#d10a1e>someone</color> definitely ate them!
&SET_POSE:Angry
	You gotta help me track down this chicken crook, man. I've been looking forward to these nuggies since I put them in the microwave.

&THINK:Arin
	<color=\#2064e3>(Dang, guess the pizza and deck-building has to wait. Ain't no way I can let my bro down.)
	Alright, Dan! Let's get this thief that's run… a-fowl!
	
&SET_POSE:Normal
&SPEAK:Dan
	Real smooth, Arin. Not your greatest, but I'll take it.
	We should check out the scene of the crime first. We might be able to find some clues that'll lead us to our guy.
&SET_POSE:AirGuitar
&PLAY_SFX:Damage1
	Then…we cluck up that son-of-a-bitch.
&SET_POSE:Normal

//-------------------------------------------[Talk With Dan]----------------------------------------//
&SCENE:Office
&ACTOR:Dan
&PLAY_SONG:InvestigationUniCore,1

&MODE:Investigation
&ADD_FAILURE_SCRIPT:INVESTIGATIONTESTFAIL1
&ADD_FAILURE_SCRIPT:INVESTIGATIONTESTFAIL2

-> Office_Choice

=== Office_Choice ===
&HIDE_TEXTBOX
+ [Introduction #Initial #Talk]
    -> Introduction
+ [About Dan #Talk]
    -> About_Dan
+ [Events #Talk]
    -> Events
+ [Suspicions #Talk]
    -> Suspicions
+ [Kitchen #Move #Kitchen]
    -> Kitchen


//-------------------------------------------[Talk With Dan: Introduction]----------------------------------------//
=== Introduction ===
&THINK:Arin
	<color=\#2064e3>(The question I keep asking myself is: who would be so dastardly as to steal Dan's dino nuggets?)
&THINK:Arin
	<color=\#2064e3>(It doesn't make sense… I trust everybody in the office to not commit such a heinous crime…)
&PLAY_SFX:Whoops
	<color=\#2064e3>(... Okay, most people.)
&PLAY_ANIMATION:Blink
&SCENE:Office
&PLAY_SFX:realization
&THINK:Arin
	<color=\#2064e3>(Or some of them, at least.)
&SET_POSE:Angry
&THINK:Arin
	<color=\#2064e3>(Or maybe just me.)
&PLAY_ANIMATION:Blink
&SCENE:Office
&PLAY_SFX:lightbulb
&THINK:Arin
	<color=\#2064e3>(Nah, actually, I would do such a thing… but not to Dan!)

&SET_POSE:Lean
&PLAY_SFX:Gavel
&SPEAK:Dan
	Hellooooooo?

&SHAKE_SCREEN:0.4,0.2
&PLAY_SFX:smack
&THINK:Arin
	Huh? What? Did you say something?

&SET_POSE:Angry
&SPEAK:Dan
	You're doing that thing where you look like you want to talk to me, but you're also staring off into space.

&PLAY_ANIMATION:Blink
&SCENE:Office
&PLAY_SFX:lightbulb
&SET_POSE:Normal
	Are you internally monologuing? Again?

&THINK:Arin
	I'm just thinking about who would have it out for you so much that they'd go and steal your nuggets…

&SET_POSE:Angry
&SHAKE_SCREEN:0.25,0.2
&PLAY_SFX:damage2
&SPEAK:Dan
	Oh God, you think this was <color=\#d10a1e>personal</color>? I didn't think about that.
	Who would do such a thing? Attacking poor defenseless dinos; what about their families?!
&SET_POSE:normal
	Quick, distract me with some questions before I think too much about it.

-> Office_Choice 
	
//---------------------------------------[Talk With Dan: About Dan]--------------------------------------//
=== About_Dan ===
&SET_POSE:normal
&THINK:Arin
	So, uh… how're you doing? 
&THINK:Arin
	Besides the whole nugget stealing thing.
	
&SPEAK:Dan
&SET_POSE:SideNormal
	The nuggets are all I can think about right now, I don't think my emotions are anything beyond that.
	I'm becoming one with the nuggets…
	
&PLAY_ANIMATION:Blink
&SCENE:Office
&PLAY_SFX:lightbulb
&THINK:Arin
	That doesn't sound as zen as you put it.
	
&SPEAK:Dan
	In the end, aren't we all just stolen nuggets baking in the oven of life?
	
&THINK:Arin
	Okaaaaaay, have fun with that existential crisis.

-> Office_Choice

//-----------------------------------------[Talk With Dan: Events]---------------------------------------------------//
=== Events ===
&THINK:Arin
	So – can you lay down everything that happened up to finding the nuggets?
	
&SET_POSE:Normal
&SPEAK:Dan
	Well, it's kind of contested, but most people believe that it all started with a powerful force of energy that started the universe–

&SHAKE_SCREEN:0.4,0.2
&PLAY_SFX:smack
&THINK:Arin
	Jeezus, skip forward a little bit, couldja? Maybe just start with this morning?
	
&SET_POSE:Angry
&SPEAK:Dan
&AUTO_SKIP:true
	Okay. This morning I headed to my office and spent the day 
&CONTINUE_DIALOGUE
&DIALOGUE_SPEED:0.09
	pretending 
&CONTINUE_DIALOGUE
&DIALOGUE_SPEED:0.03
&AUTO_SKIP:false
	to read emails whilst singing solo karaoke.
	I got hungry so I went into the kitchen and prepared my dino nuggets. <color=\#d10a1e>Nobody was in there</color>.
	I went back to my office for <color=\#d10a1e>ten minutes</color>, killed some time and then headed back in…
&SET_POSE:SideNormal
	The plate was on the side…
	There were <color=\#d10a1e>crumbs</color> everywhere…
	
&THINK:Arin
	It's okay, Dan. I know this is hard for you. 
	
&SPEAK:Dan
&PLAY_SFX:Whoops
	Thanks, man… I just really want those nuggets back safe and sound…
	
&PLAY_ANIMATION:Blink
&SCENE:Office
&PLAY_SFX:realization
&THINK:Arin
	And <color=\#d10a1e>pre-digested</color>?
	
&SET_POSE:Angry
&SPEAK:Dan
	Yeah, that too.

-> Office_Choice
	
//---------------------------------------------[Talk With Dan: Suspicions]------------------------------------------//
=== Suspicions ===
&SET_POSE:Normal
&THINK:Arin
	Do you think there's anybody who coulda dunnit? 

&SHAKE_SCREEN:0.2,0.35
&PLAY_SFX:smack
&SET_POSE:Angry
&SPEAK:Dan
	I don't know, man! Who could have it out for those poor, innocent nuggets?
&SET_POSE:SideNormal
	They were so young… so full of nutrition…
	
&THINK:Arin
	They're in a better place now, Dan.
	
&SHAKE_SCREEN:0.2,0.35
&PLAY_SFX:stab
&SET_POSE:angry
&SPEAK:Dan
	My <color=\#d10a1e>stomach</color> would be a better place, Arin!

-> Office_Choice

//----------------------------------------[Kitchen]---------------------------------------------------//
=== Kitchen ===
&FADE_OUT:1
&HIDE_ACTOR

&MODE:Dialogue
&SCENE:Kitchen
&PLAY_SONG:InvestigationJoonyer,1
&FADE_IN:2
&SHOW_ACTOR
&ACTOR:Dan
&SET_POSE:Normal
&THINK:Arin
	As we stepped into the scene of the crime, I knew that this might be the toughest case of my entire career…
	
&SET_POSE:Lean
&PLAY_SFX:realization
&WAIT:0.1
&SCENE:Kitchen
&SPEAK:Dan
	Are you… narrating yourself?
	
&THINK:Arin
	… said Dan.
	
&SET_POSE:Angry
&SPEAK:Dan
	Dan pointed out to Arin that narrating his every action would just distract him from the job, and he ought to have a look around the crime scene for clues.
	
&PLAY_SFX:Whoops
&THINK:Arin
&AUTO_SKIP:true
	Okay. 
&CONTINUE_DIALOGUE
&AUTO_SKIP:false
&WAIT:0.25
	What do I have to look for?
	
&SET_POSE:Normal
&SPEAK:Dan
	Well, <color=\#d10a1e>hard evidence</color> would be great. Also, any <color=\#d10a1e>key witnesses</color> who might have seen the crime being committed…

&NARRATE
<color=\#5ee320> [Burgie slides into frame]

&ACTOR:Burgie
&SET_POSE:Normal
&SPEAK:Burgie
	Oh hey dudes, stayin' beefy?

&PLAY_ANIMATION:Blink
&SCENE:Kitchen
&PLAY_SFX:lightbulb

//----------------------Actors edited out until two can share a screen.----------------------//

//&ACTOR:Dan
//&SET_POSE:Normal

&THINK:Arin
	Burgie! How's it going? 

&SPEAK:Burgie
	Takin' it cheesy, boss.
	What're you doing around these ends, eh? Don'tcha usually just order your food on the app then eat alone in your office watching anime?
	
&SHAKE_SCREEN:0.2,0.35
&PLAY_SFX:stab2
&THINK:Arin
	No!!
	...

&SHAKE_SCREEN:0.2,0.2
&PLAY_SFX:smack
	That's irrelevant! We're investigating a crime scene.

//&ACTOR:Dan
//&SET_POSE:Normal
&SPEAK:Burgie
	A crime, eh? Well, I ain't got nothin' to do with nothin', alright?
	And I ain't talkin' to no lawyer without my lawyer present, got it?

&ACTOR:Dan
&SET_POSE:Angry
&SPEAK:Dan
	Uh, I'm pretty sure you mean you're not talking to cops without a lawyer present.
	And if anyone was gonna be your lawyer, it would probably be us!

&ACTOR:Burgie
&SET_POSE:normal
&SPEAK:Burgie
	I have a lotta beef with that statement, Danny.

&SHAKE_SCREEN:0.2,0.4
&PLAY_SFX:smack
&THINK:Arin
	Look; <color=\#d10a1e>somebody</color> stole Dan's dino nuggets, and we've come to investigate the scene of the crime.

&ACTOR:Dan
&SET_POSE:Angry
&SPEAK:Dan
	You were here when I came in to get my food. You didn't happen to see anything, did you?

&ACTOR:Burgie
&SET_POSE:normal
&SPEAK:Burgie
	Sure, I mighta seen a thing or two. If you gotta ask me some Q's, then ask me some Q's. 
	Just don't take too long. I'm on my onion regulated break.

//&ACTOR:Dan
//&SET_POSE:Normal
&PLAY_ANIMATION:Blink
&SCENE:Kitchen
&PLAY_SFX:lightbulb
&THINK:Arin
	Don't you mean <color=\#d10a1e>“union break”</color>?

&ACTOR:Burgie
&SET_POSE:normal
&SPEAK:Burgie
	Why you all up in my grill? I said what I said.
	
//---------------------------------------------------[Kitchen: Talk]---------------------------------------------------//
&MODE:Investigation
&ADD_FAILURE_SCRIPT:INVESTIGATIONTESTFAIL1
&ADD_FAILURE_SCRIPT:INVESTIGATIONTESTFAIL2

-> Kitchen_Choice

=== Kitchen_Choice ===
&HIDE_TEXTBOX
+ [The Nuggets #Talk]
    -> The_Nuggets
+ [What you witnessed #Talk]
    -> What_you_witnessed
+ [Owed Rent #Talk]
    -> Owed_Rent
+ [This Kitchen #Talk]
    -> This_Kitchen
+ [Office #Move #Office]
    -> Office_Choice
+ [Reception #Move #Reception]
    -> Reception

=== The_Nuggets ===
&THINK:Arin
	Dan said you were in the kitchen when he found the nuggets were missing…
&SHAKE_SCREEN:0.3,0.2	
&PLAY_SFX:smack
	Was it YOU who stole those dinosaur nuggets?!
	
&SHAKE_SCREEN:0.3,0.35
&PLAY_ANIMATION:Blink
&SCENE:Kitchen
&PLAY_SFX:stab
&SPEAK:Burgie
	Blech! You think I'd eat some <color=\#d10a1e>garbage</color> like that?!
	
&ACTOR:Dan
&SET_POSE:Angry
&AUTO_SKIP:true
&DIALOGUE_SPEED:0.08
&SPEAK:Dan
	You take that back you
&AUTO_SKIP:true
&CONTINUE_DIALOGUE
&WAIT:0.3
&SHAKE_SCREEN:0.1,0.2
&PLAY_SFX:damage1	
	sick 
&CONTINUE_DIALOGUE
&AUTO_SKIP:false
&WAIT:0.35
&SHAKE_SCREEN:0.25,0.35
&PLAY_SFX:stab	
	FUCK.

&DIALOGUE_SPEED:0.03
&ACTOR:Burgie
&SET_POSE:normal
&SPEAK:Burgie
	 Y'think these meaty flaps can digest food like deep-fried chicken, pal? Anthropomorphic burgers can't just eat whatever's in the bodega, ya know?
	 Burgers can only eat <color=\#d10a1e>ketchup</color>, <color=\#d10a1e>mustard</color> and <color=\#d10a1e>relish</color>. Exclusively. Scientifically backed.
&SET_POSE:thinking
&WAIT:1.5
&SET_POSE:normal
	 … oh, and <color=\#d10a1e>pizza</color>.
	 
&ACTOR:Dan
&SET_POSE:Normal
&SPEAK:Dan
	 It checks out, I've never seen Burgie eat anything else. It must have been somebody else.
	 
&THINK:Arin
	<color=\#2064e3>(Well, who am I to question burger biology?)
	
-> Kitchen_Choice

=== What_you_witnessed ===
&ACTOR:Dan
&SET_POSE:normal
&SPEAK:Dan
&AUTO_SKIP:true
	If you've been in the room since they went missing, 
&CONTINUE_DIALOGUE
&WAIT:0.2	
&AUTO_SKIP:false
	you must have seen somebody take them, right?
	 
&ACTOR:Burgie
&SET_POSE:normal
&SPEAK:Burgie
	 I got too much on my plate to worry about other people's lunch, boss.
	 I came in <color=\#d10a1e>a few minutes ago</color> to top myself up on that relish. If someone stole them nuggets, it musta' been <color=\#d10a1e>before I came in</color>.
	 
&PLAY_ANIMATION:Blink
&SCENE:Kitchen
&PLAY_SFX:realization
&THINK:Arin
	I kinda hate to ask but… how exactly do you “top yourself up”?
	
&SET_POSE:Normal
&PLAY_SFX:lightbulb
&SPEAK:Burgie
	Oh boy, glad you asked.
	It's all about the aim; if I squirt in the wrong place, it's a disaster. Like this one time–
	
&ACTOR:Dan
&SET_POSE:Angry
&SHAKE_SCREEN:0.4,0.7
&PLAY_SFX:Damage2
&SPEAK:Dan
	 Okay, that's definitely enough of that! Can we get back on topic?!

-> Kitchen_Choice
	 
=== Owed_Rent ===
&ACTOR:Burgie
&THINK:Arin
	Hey, just so you know – you still owe me $100 in rent.
	
&SHAKE_SCREEN:0.25,0.35
&PLAY_ANIMATION:Blink
&SCENE:Kitchen
&PLAY_SFX:damage2
&SPEAK:Burgie
	Ey, <color=\#d10a1e>fuck you</color> and <color=\#d10a1e>fuck landlords</color>, pal!
	
&THINK:Arin
	<color=\#2064e3>(Oooo-kay, I'll ask another time.)

-> Kitchen_Choice

=== This_Kitchen ===
&ACTOR:Dan
&SPEAK:Dan
	Did you see anybody coming in or out of the kitchen at all? Or even hanging around nearby?

&ACTOR:Burgie
&SPEAK:Burgie
	Eh, well, I was hangin' around outside moppin' the floor. Got to see all sorts.
	I did see this freakishly tall guy with curly hair come bargin' in, screamin' bloody murder about his kid-friendly dino nuggets being stolen.

&ACTOR:Dan
&SET_POSE:Angry
&SPEAK:Dan
&SHAKE_SCREEN:0.25,0.35
&PLAY_SFX:smack
	Oh, you're a real wise guy, aren't you?
	
&ACTOR:Burgie
&THINK:Arin
	C'mon, Burgie. Did you see anybody else at all?
	
&SPEAK:Burgie
I did see that <color=\#d10a1e>weird chick from reception</color> walkin' outta here… you know, the pocket-sized gal with the huge glasses?

&ACTOR:Dan
&PLAY_ANIMATION:Blink
&SCENE:Kitchen
&PLAY_SFX:lightbulb
&SPEAK:Dan
He has to be talking about <color=\#d10a1e>Laura</color>! 

&ACTOR:Burgie
&SPEAK:Burgie
&AUTO_SKIP:true
	That's the one. Every time I try to talk to her, 
&SET_POSE:nervous
&CONTINUE_DIALOGUE
&AUTO_SKIP:false
	she just tries to pitch whatever sitcom she's writin' that week.
&SET_POSE:sweaty
	Do I look like a fuckin' TV producer to her? I'm just here to mop the floor and rep the labor union.

&NARRATE
&PLAY_SFX:EvidenceDing
<color=\#5ee320> ["Burgie's Testimony" Added to Court Record.]

&THINK:Arin
<color=\#2064e3>(I'm surprised that she hasn't tried to hit it off with Burgie. Or maybe her standards are above walking, talking burger men?)
<color=\#2064e3>(I get the feeling she's more into hot dogs than burgers.)

-> Kitchen_Choice
//---------------------------------------------------[Kitchen:Examine]---------------------------------------------------//

&NARRATE
	<color=\#5ee320>[Examine Kitchen]<br>[Empty Nugget Plate]
&ACTOR:Dan
&THINK:Arin
	Well, here's the plate. And no chicken nuggets in sight.
&SET_POSE:SideNormal
&SPEAK:Dan
	I can't believe someone would take them, Arin. I'm so betrayed right now.
	
&NARRATE
&PLAY_SFX:EvidenceDing
	<color=\#5ee320>["Empty Nugget Plate" Added to Court]<br>[Record.]

&THINK:Arin
	There's a lot of <color=\#d10a1e>crumbs</color> left on the plate. Whoever ate them must have really enjoyed them.
	
&SET_POSE:Angry
&SHAKE_SCREEN:0.2,0.35
&PLAY_SFX:stab2
&SPEAK:Dan
	Alright, jeez, no need to rub salt in the wound! Just take the damn plate and stop reminding me…
	
&NARRATE
<color=\#5ee320> [Examine Kitchen]<br> [Unused Cutlery]

&SET_POSE:normal
&THINK:Arin
	Er, Dan… I don't mean to judge, but–
	
&SET_POSE:Angry
&SHAKE_SCREEN:0.2,0.35
&PLAY_SFX:smack
&SPEAK:Dan
	I'm gonna stop you right there, Arin, because it <color=\#d10a1e>sounds</color> like you're about to judge.

&THINK:Arin
	I'm not judging, I just didn't realize you treated these dino nuggets as a fine dining experience.

&SPEAK:Dan
&SET_POSE:Normal
&AUTO_SKIP:true
	I treat
&CONTINUE_DIALOGUE
&DIALOGUE_SPEED:0.12
	everything
&CONTINUE_DIALOGUE
&DIALOGUE_SPEED:0.03
&AUTO_SKIP:false
	 in life as a fine dining experience.
&PLAY_ANIMATION:Blink
&SCENE:Kitchen
&PLAY_SFX:lightbulb
&SET_POSE:Angry
	Especially pre-packaged dinosaur nuggets.
	
&PLAY_SFX:realization
&AUTO_SKIP:true
&THINK:Arin
...?
&AUTO_SKIP:false
&WAIT:1
&THINK:Arin
	It's a bit odd that these are so clean, though. And the napkin hasn't been removed either. Whoever ate your nuggets <color=\#d10a1e>didn't use these</color>.

&SET_POSE:Normal
&SPEAK:Dan
	But they come out of the microwave really hot. I mean <color=\#d10a1e>seriously hot</color>. You'd have to have fingers made of steel to survive touching them.

&NARRATE
&PLAY_SFX:EvidenceDing
	<color=\#5ee320> ["Unused Cutlery" Added to Court Record.]
	
&THINK:Arin
	What about just plunging your face straight into the plate instead?

&SET_POSE:Angry
&SPEAK:Dan
&SHAKE_SCREEN:0.2,0.35
&PLAY_SFX:smack
	Don't be ridiculous, Arin.

&NARRATE
<color=\#5ee320> [Examine Kitchen]<br> [Trash (nugget packaging)]

&SET_POSE:Lean
&SPEAK:Dan
	Arin, what have I told you about poking around the trashcan?
	
&SHAKE_SCREEN:0.4,0.35
&PLAY_SFX:damage1
&THINK:Arin
	I'm not! I was pulling <color=\#d10a1e>this</color> out.
	
&NARRATE
&PLAY_SFX:EvidenceDing
	<color=\#5ee320> ["Nugget Packaging" Added to Court Record.]
	
&SHAKE_SCREEN:0.3,0.5
&PLAY_SFX:shock2
&THINK:Arin
	Jiminy Christmas! You're telling me these nuggets come out the microwave at <color=\#d10a1e>300°F</color>?!
	
&SET_POSE:Normal
&SPEAK:Dan
	Yep. I actually considered putting a silver lining in my stomach so I could just eat them straight.
&SET_POSE:Angry
	Unfortunately, there's no such thing. So I have to let it cool instead.
	
&THINK:Arin
	Well, whoever ate your nuggets <color=\#d10a1e>didn't wait</color>… 

//---------------------------------------------[Reception:Introduction]-----------------------------------------------//
=== Reception ===
&MODE:Dialogue
&FADE_OUT:1
&NARRATE
	<color=\#5ee320>[Player moves to the reception]
&NARRATE
	<color=\#5ee320>[Fade to the reception desk. It is a large semi-circle in front of a wall emblazoned with ‘Groomp Spiarmf Buttlet Scrumf Smeef Cramwell Slurmp CD-I Tree-Head Qoxonmfase Spuunt Spoompls Spantzz Squackle Jones LLP ‘in large, three dimensional, sparkly letters.]
	<color=\#5ee320>[For added hilarity, it can just continue off screen and get cut off at some point. Laura sits at the desk with a computer screen and a banana phone.]
	<color=\#5ee320>[There is a sign in sheet on the counter in front of her on a penguin shaped clipboard]
&HIDE_ACTOR
&SCENE:Reception
&PLAY_SONG:InvestigationUniCore,1
&FADE_IN:2
&SHOW_ACTOR
&ACTOR:Dan
&SPEAK:Dan
	Ooh, might stop at the vending machine whilst we're here…
	
&SHAKE_SCREEN:0.2,0.35
&PLAY_SFX:smack
&THINK:Arin
	No way! Not whilst we're on a roll and being, uh… productive!
	
&SHAKE_SCREEN:0.2,0.35
&PLAY_SFX:stab2
&SET_POSE:Angry
&SPEAK:Dan
	My 4<link=character>:</link>20 snack got <color=\#d10a1e>stolen</color>, Arin, I haven't been able to eat! I can't do detective work on an empty stomach!

&THINK:Arin
	Don't worry, I've already got that covered.
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
&THINK:Arin
	We'll just make the thief regurgitate your dino nuggies back onto the plate. Problem solved!

&PLAY_SFX:Whoops
&SPEAK:Dan
	Aaaand on that mental image, my appetite has gone. Thanks, Arin.
	Obviously we should talk to Laura and see what she knows. Either she saw something, or she's our culprit.

&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:realization
&THINK:Arin
	Yup. I think we should poke around for evidence, too. I'm sure I'm allowed to ‘borrow' a few items.
	
&SET_POSE:normal
&SPEAK:Dan
	Well, so long as we find our clucking crook, I won't tell. Let's go!
	
&PLAY_SFX:realization
&PLAY_SONG:SimplePlotOfInvestigation,2
&ACTOR:Laura
&SET_POSE:sweaty
&SPEAK:Laura
	Guuuuuys, I was just about to come find yooooou. 
&SET_POSE:ThumbsUp
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
	I have a greeaaaat idea for a soap opera about three penguins and a door stooooop–
	
&SHAKE_SCREEN:0.2,0.35
&PLAY_SFX:smack
&SET_POSE:nervous
&THINK:Arin
&AUTO_SKIP:true
	Let me stop you right there, Laura. 
&WAIT:0.75
&CONTINUE_DIALOGUE
&AUTO_SKIP:false
&SET_POSE:sweaty
	I don't care.

&SHAKE_SCREEN:0.4,0.2
&PLAY_SFX:damage2
&ACTOR:dan
&SET_POSE:Angry	
&SPEAK:Dan
&AUTO_SKIP:true
	We're hot on the trail of a nugget thief and 
&CONTINUE_DIALOGUE
&AUTO_SKIP:false	
&SHAKE_SCREEN:0.15,0.2
&PLAY_SFX:stab
	YOU'RE our next suspect!

&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:stab2
&ACTOR:Laura
&SET_POSE:sweaty
&SPEAK:Laura
	Meeeee?!
	Oh my Gaaaaawd!
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:realization
&SET_POSE:Thinking
	This is like one of those crime shoooows I see on TV all the tiiiiime!
&SET_POSE:ThumbsUp
&PLAY_SFX:lightbulb
	Well it's just your luck, ‘cause you're speaking to America's next silver screen icooooon!
&SET_POSE:nervous
&PLAY_SFX:Whoops
	If only BBC would answer my caaaaalls… //Was NBC but BBC is funnier?
	
&SHAKE_SCREEN:0.2,0.35
&PLAY_SFX:smack
&THINK:Arin
	Well, we've got a whole buncha' questions and we demand a whole buncha' answers. You better be ready to fess up!
&SET_POSE:ThumbsUp
&SPEAK:Laura
	I've always wanted to be interrogateeeeed by two mysterious but charming lawyeeeeers…
&SET_POSE:Normal
	…but I suppose you two will doooooo.
	
//---------------------------------------------------[Laura:Talk]---------------------------------------------------//
&NARRATE
&AUTO_SKIP:true
	<color=\#5ee320>[The Investigation Menu unlocks, allowing Talk, Examine, Move, and Present]
&CONTINUE_DIALOGUE
&AUTO_SKIP:false
	</color><br><color=\#5ee320>[The Player selects Talk]
	<color=\#5ee320>[The Player will recieve branching prompts to build a phrase to ask, one selection at a time]
	<color=\#5ee320>[Only the critical path will be represented in the script until branching is implemented]
//TODO:Convert to branching mechanics once implementable.
	<color=\#2064e3>...<br><color=\#5ee320>[Hello...]</color><br><color="yellow">[>Tell me...]</color><br><color=\#5ee320>[Can you...]</color>
	<color=\#2064e3>Tell me...</color><br><color=\#5ee320>[about...]</color><br><color="yellow">[>at the time of the theft...]</color><br><color=\#5ee320>[who...]</color>
	<color=\#2064e3>Tell me, at the time of the theft...</color><br><color="yellow">[>where were you?]</color><br><color=\#5ee320>[what were you doing?]</color>

&ACTOR:Dan
&SPEAK:Dan
&PLAY_SFX:lightbulb
	<color=\#5ee320>Tell me, at the time of the theft where were you?

&ACTOR:Laura
&SPEAK:Laura
	Well around that time I get pretty huuuungry so I went to the kitchen to grab a snack from the—
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:realization
&STOP_SONG
&SET_POSE:nervous
&WAIT:2
&SHAKE_SCREEN:0.1,0.2
&PLAY_SFX:damage1
&SET_POSE:sweaty
	…!
	
&ACTOR:Dan
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:realization
&SET_POSE:Angry
&PLAY_SONG:FyiIWannaXYourExaminationModerato,1
&SPEAK:Dan
	<color=\#d10a1e>A snack</color>? What was that last part?

&ACTOR:Laura
&SET_POSE:sweaty
&WAIT:1.2
&PLAY_ANIMATION:Blink
&SCENE:Reception
&SHAKE_SCREEN:0.3,0.4
&PLAY_SFX:stab2
&SET_POSE:ThumbsUp
&SPEAK:Laura
	Who said anything about a snaaaaaack? 
&AUTO_SKIP:true
	I don't need a snaaaack; 
&WAIT:0.5
&CONTINUE_DIALOGUE
	I'm already a snaaaaack!
&WAIT:0.5
&SET_POSE:sweaty
&WAIT:0.5
&SET_POSE:ThumbsUp
&CONTINUE_DIALOGUE
&AUTO_SKIP:false
	...
&ACTOR:Dan
&SET_POSE:Angry
&SPEAK:Dan
	We both just heard you say you were getting a snack five seconds ago. //this line is rewritten, original line felt awkward in the sequence
//-----------------------------------------[Gauntlet 1: Introduction]--------------------------------------------//
&ACTOR:Laura
&SET_POSE:sweaty
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:shock2
&SPEAK:Laura
	NOOOOO! I wasn't getting a snaaaaack! 
&SET_POSE:ThumbsUp
	Well I am a snaaaaack, but I wasn't getting a snaaaaack! 
	<color=\#5ee320>What makes you think I ate anythiiiing?</color>
&NARRATE
&AUTO_SKIP:true
	<color=\#5ee320>[The player must present evidence from the Court Record]
&CONTINUE_DIALOGUE
&AUTO_SKIP:false
	<color=\#5ee320><br>[The player does not select a talking point at this point in the tutorial]
	<color=\#5ee320>[Present "Empty Nugget Plate"]
&PLAY_SFX:TakeThatArin
&THINK:Arin
	If you had a snack, that could explain why this plate is so jingle jangle empty! //rewritten from original line to better segway the accusation from her defense

&SET_POSE:Normal
&SPEAK:Laura
	Oh Ariiiiin, there's <color=\#d10a1e>always</color> empty plates in the kitcheeeeen. That's where they gooooo.
	
&SHAKE_SCREEN:0.3,0.4
&PLAY_SFX:whoops
&THINK:Arin
	Don't condescend me!
	
&SPEAK:Laura
	I don't know how elseeee to talk to yooooou.
//-------------------------------------------[Gauntlet 1: Talking Points]---------------------------------------------//
&ACTOR:Dan
&THINK:Dan
	<color="blue>(This isn't getting us anywhere… deductive reasoning isn't enough!) 
&AUTO_SKIP:true
&THINK:Dan
	<color="blue>(It's time to turn on that Avidan charm and use some 
&CONTINUE_DIALOGUE
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
&AUTO_SKIP:false
&THINK:Dan
	<color="yellow">seductive reasoning</color><color="blue> to talk our way through this!)
&NARRATE
	<color=\#5ee320>[The UI changes, revealing the three 'locks' that prevents Laura from telling us the truth]
&CONTINUE_DIALOGUE
	<br><color=\#5ee320>[Note: We may reflavor 'locks']

&ACTOR:Laura
&SET_POSE:normal
&NARRATE
	<color=\#5ee320>[The player receives a tutorial on adding a talking point to the evidence they present]
&AUTO_SKIP:true
	<color=\#5ee320>[Present:"Empty Nugget Plate"]
&CONTINUE_DIALOGUE
	<br><color=\#5ee320>[There are still nuggie remains on this plate!]
&CONTINUE_DIALOGUE
	<br><color=\#5ee320>[It's still warm from being in the microwave!]
&CONTINUE_DIALOGUE
&AUTO_SKIP:false
	<br><color="yellow">[>You were the last person in the kitchen!]

&PLAY_SFX:TakeThatArin
&THINK:Dan
	This isn't just any empty plate! Burgie's account says you were the last person in the kitchen before the nuggies vanished!

//--------------------------------------[Gauntlet 2: Not enough evidence]---------------------------------------//
	
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
&SET_POSE:nervous
&THINK:Dan
	If you were the last person, you HAD to have eaten them!
	
&NARRATE
	<color=\#5ee320>[The first of three 'locks' break, the player moves into the second gauntlet]
&SET_POSE:sweaty
&PLAY_SFX:stab
&SPEAK:Laura
	Oh boys, I'm suuuuure <color=\#d10a1e>I wasn't the only one in the kitchen</color> when those poor dinos went missiiiiiing.
	It's a shared kitcheeeen. Lots of people go iiiiin and ooooout.
	
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:realization
&THINK:Dan
	True, but it doesn't tell us where those nuggies went, does it?
	
&SPEAK:Laura
&SET_POSE:nervous
	Weeeeell, I don't wanna tattle on our fellow coworkeeeeers…
&SET_POSE:ThumbsUp
	Which reminds me of a really great storyyyy when I caught my old boss having an affaiiiiiir with HR…

&SHAKE_SCREEN:0.3,0.4
&THINK:Dan
&SET_POSE:sweaty
&PLAY_SFX:damage2
	Skip to the part where you answer the question!
	
&SPEAK:Laura
&SET_POSE:normal
	Okaaay, but you're missing out on some really good teeeeea…
&SET_POSE:nervous
	I see Burgie in the kitchen a lot, you knooow. And I see <color=\#d10a1e>Ross</color> there alot too.
	I call him Peeeeerth, because he's three hours behind everybody elseeeee. Heehee.
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
&SET_POSE:thinking
	Maybe he took them? He does have that criminal energyyyyy!
	
&ACTOR:Arin
&SET_POSE:Thinking
&SPEAK:Arin
	But did you see either of them when you were getting a snack, while the nuggets were there?
	
&ACTOR:Laura
&SET_POSE:Normal
&SPEAK:Laura
	Oh silly! I didn't get a snaaaaack, not at aaaaaall. 
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
	I was only in there for a few momeeeeents before the office phoneeeee on my deeeeesk started to riiiiiing. 
	<color=\#5ee320>I didn't see a soooooul in there since I've been stuck at my desk aaaaaall daaaaaaay. //extended original line to better segway to the correct evidence

&NARRATE
	<color=\#5ee320>[In this version of events, the player has yet to collect the evidence to refute this point and must manually exit]
&SET_POSE:Thinking
&SPEAK:Laura
	Are weeeee doneeee? I gotta get back to scrolling on my dating aaaapps. The lack of gentlemen in Attitude City is appaaaalling.
&THINK:Dan
	<color=\#2064e3>(I hate to admit it, but Laura's a tough nut to crack… is there something around the office that I'm missing?) //original line was Arin's, but Dan is the one doing the primary cracking
&THINK:Dan
&SET_POSE:normal
	We'll be back soon, and then we'll make this as slow as it needs to be. 
&HIDE_ACTOR
&PLAY_SONG:SimplePlotOfInvestigation,2
//-------------------------------------------[Reception: Examine]---------------------------------------------------//
	
&NARRATE
	<color=\#5ee320>[Player returns to investigation menu and selects >Examine]
	<color=\#5ee320>[Examine: Office Register]

&SHOW_ACTOR
&ACTOR:Dan
&SET_POSE:Normal
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
&SPEAK:Dan
	Aw, that clipboard is adorable. It's a little penguin!
	
&THINK:Arin
	Oh, I didn't think you'd be into that. I can buy you one as a ‘Sorry You Got Robbed' gift?
	
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:realization
&SPEAK:Dan
&SET_POSE:Angry
	Is that a thing? Do people send condolence cards if someone gets robbed? Or for any major life issue that occurs?
&SET_POSE:normal
	Like, imagine a card that says: ‘Sorry to hear your cat got run over by a car. 
&CONTINUE_DIALOGUE
	<br>What a cat-astrophe!'
	
&THINK:Arin
	I can't decide if I'd be offended, or find it really, really funny.

&SPEAK:Dan
	Either way, I'll pass on the penguin clipboard, thanks. I don't use clipboards enough to justify one.
	I do have a stegosaurus one for emergencies though!

&NARRATE
&PLAY_SFX:EvidenceDing
	<color=\#5ee320> ["Office Register" Added to Court Record.]
	
&THINK:Arin
	This has a list of everybody who's signed in and out of the office today, staff and visitors included.
&PLAY_SFX:Whoops
	It's… a little empty.
	
&SPEAK:Dan
&SET_POSE:Angry
&SHAKE_SCREEN:0.3,0.4
&PLAY_SFX:smack
	A little empty? There's only <color=\#d10a1e>four names</color> on this list! And two of them are ours!
	
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:stab2
&THINK:Arin
	We do a lot of business over the phone, alright?!
	
&NARRATE
	<color=\#5ee320>[Examine: Office Phone]
	
&SET_POSE:normal
&SPEAK:Dan
	Oh, a banana. Finally, something to eat.
	
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
&THINK:Arin
	It's not a fruit, Dan. That's our <color=\#d10a1e>office phone</color>.
	
&SPEAK:Dan
&SET_POSE:sideNormal
	Aw. Snap.
&SET_POSE:normal
	Funny design though.
	
&THINK:Arin
	You know, I can check to see what calls have been going in and out. Could be good evidence.

&SET_POSE:lean
&SPEAK:Dan
	I'm betting you also wanna snoop on Laura's business, right?

&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
&THINK:Arin
	You betcha!
&THINK:Arin
	…
&THINK:Arin
	There aren't a lot of incoming calls on this thing. We <color=\#d10a1e>haven't had any today</color>…

&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:realization
&WAIT:1
&THINK:Arin
	I'm concerned about the amount of <color=\#d10a1e>outgoing calls</color> though…
	
&NARRATE
&PLAY_SFX:EvidenceDing
	<color=\#5ee320> ["Office Phone" Added to Court Record.]

&SET_POSE:normal
&SPEAK:Dan
	Well, as a law firm, I'm pretty sure we have to contact a lot of our clients directly about any new information we get, right?
	
&THINK:Arin
	I'd agree with you if most of these weren't dialed to ‘Donnie Jepp, love heart, love heart, kiss, kiss.'

&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
&THINK:Arin
	Like, it's literally spelled out that way.

&SPEAK:Dan
&SET_POSE:angry
	Oh my God, is Laura finally seeing somebody?

&THINK:Arin
	Well, Donnie Jepp hasn't picked up even once, so…
	
&SET_POSE:normal
&SPEAK:Dan
	I'll assume that's a big no then.
	
//---------------------------------------[Gauntlet 2: Now enough evidence]-------------------------------------//

&ACTOR:Laura
&NARRATE
	<color=\#5ee320>[Player has collected all the evidence they need, returning to Laura]
	<color=\#5ee320>[A shortcut to activating Seductive Reasoning will be at the top of "Talk" for the player to select and return quickly]

&ACTOR:Dan
&NARRATE
&PLAY_SFX:lightbulb
	<color="yellow">[>Tell me, at the time of the theft where were you?]</color><br><color=\#5ee320>[Hello...]</color><br><color=\#5ee320>[Tell me...]</color><color=\#5ee320><br>[Can you...]</color>
&SPEAK:Dan
&SET_POSE:Angry
&PLAY_SONG:FyiIWannaXYourExaminationAllegro,1
	<color=\#5ee320>Tell me, at the time of the theft where were you?
	We know you were in the kitchen, if anyone else went in there you should have seen them!
&ACTOR:Laura
&SET_POSE:nervous
	I see Burgie in the kitchen a lot, you knooow. And I see <color=\#d10a1e>Ross</color> there alot too.
	I call him Peeeeerth, because he's three hours behind everybody elseeeee. Heehee.
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
&SET_POSE:thinking
	Maybe he took them? He does have that criminal energyyyyy!
	
&ACTOR:Arin
&SET_POSE:Thinking
&SPEAK:Arin
	But did you see either of them when you were getting a snack, while the nuggets were there?
	
&ACTOR:Laura
&SET_POSE:Normal
&SPEAK:Laura
	Oh silly! I didn't get a snaaaaack, not at aaaaaall. 
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
	I was only in there for a few momeeeeents before the office phoneeeee on my deeeeesk started to riiiiiing. 
	<color=\#5ee320>I didn't see a soooooul in there since I've been stuck at my desk aaaaaall daaaaaaay. //extended original line to better segway to the correct evidence
	
//--------------------------------------------[Gauntlet 2: Present Phone]------------------------------------------//
	
&NARRATE
&AUTO_SKIP:true
	<color=\#5ee320>[Present:"Office Phone"]<br>
&CONTINUE_DIALOGUE
	<color=\#5ee320>[This phone is clearly a banana.]<br>
&CONTINUE_DIALOGUE
	<color="yellow">[>No calls have come in today.]<br>
&CONTINUE_DIALOGUE
&AUTO_SKIP:false
	<color=\#5ee320>[No one has been at the desk at all!]
	
&SHAKE_SCREEN:0.2,0.3
&PLAY_SFX:TakeThatArin
&THINK:Dan
&SET_POSE:nervous
	Important phone calls? The phone hasn't rung <color=\#d10a1e>since yesterday</color>!
&THINK:Dan
	According to the phone log, only calls you've been making are to that Donnie Jepp guy!
&THINK:Dan
	So, since you weren't on a call, you didn't see anybody enter because no one came in!
&SHAKE_SCREEN:0.4,0.3
&PLAY_SFX:damage2
&SET_POSE:sweaty
&SPEAK:Laura
	!!!!
&SHAKE_SCREEN:0.2,0.3
&PLAY_SFX:stab2
	W-Weeeeell, I didn't see anyone come iiiiiin, because they were <color=\#d10a1e>already heeeere</color>. 
	I saw that outback steakmuffin Roooooss walking by on his way to the bathroom, I couldn't miss him if I triiiiiied. 
&SET_POSE:ThumbsUp
	And I dooooon't.
	
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
&THINK:Dan
&AUTO_SKIP:true
	Ross is here? 
&CONTINUE_DIALOGUE
&WAIT:0.3
&AUTO_SKIP:false	
	I thought he had an appointment today for cup size enhancement for his butt implants?
	
&ACTOR:Arin
&SET_POSE:sweaty
&SPEAK:Arin
	Wait, what?
	
&THINK:Dan
	Oh yeah, he had a real Hank Hill ass from birth. The surgery really turned the other cheek for him.
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:realization
&THINK:Dan
	<color=\#2064e3>(Do I have any solid evidence to prove that Ross wasn't here today?)
	
//-----------------------------------------[Gauntlet 2: Present Register]------------------------------------------//
	
&ACTOR:Laura
//All of the register points reference Ross specifically, which would only ever make sense in this exact scenario. 
//Changed the prompts to be about the evidence itself but will require some small rewrites.
&NARRATE
&AUTO_SKIP:true
	<color=\#5ee320>[Present:Office Register]<br> 
&CONTINUE_DIALOGUE
	<color="yellow">[>Laura and Burgie are the only others here]<br>
&CONTINUE_DIALOGUE
	<color=\#5ee320>[The register is updated manually by Laura]<br>
&CONTINUE_DIALOGUE
&AUTO_SKIP:false
	<color=\#5ee320>[No strangers have entered the building today]<br>

&SHAKE_SCREEN:0.2,0.3
&PLAY_SFX:TakeThatArin
&THINK:Dan
	Ross' name isn't on this sheet, he hasn't been in the office today!
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:shock2
&SET_POSE:nervous
&THINK:Dan
	You're just trying to throw the blame onto somebody else! Someone who was connected to ANOTHER dinosaur crime…
&SHAKE_SCREEN:0.3,0.5
&PLAY_SFX:stab2
&THINK:Dan
	Because YOU were the one who stole the nuggets!
	
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:damage2
&SPEAK:Laura
&SET_POSE:sweaty
	!!!!
	
&NARRATE
	<color=\#5ee320>[The second of three 'locks' break, the player moves into the final gauntlet]
	<color=\#5ee320>[Getting to this point of the story unlocks new dialogue options with Burgie]
//----------------------------------------[Gauntlet 3: Burgie's Implication]----------------------------------------//
&SPEAK:Laura
	I never <color=\#d10a1e>said</color> Ross stole the nuggeeeeets! I must have forgotten what day I saw hiiiiim!!
&SHAKE_SCREEN:0.3,0.35
&PLAY_SFX:stab2
	But if he wasn't hereeee then it has to be Burgieeeeee! He's <color=\#d10a1e>the only other person</color> in the buildiiiiiiing!

&THINK:Dan
	<color="blue>(Laura keeps changing her story, do I have evidence to prove it couldn't have been Burgie?)

&NARRATE
	<color="green>[The Player won't have the evidence to continue at this point, and must exit to go talk to Burgie again to get a clearer version of his testimony.]

&THINK:Dan
	<color=\#2064e3>(I need to know more about what Burgie saw if I'm going to be able to get any more information out of her.)
&ACTOR:Arin
&SET_POSE:confident
&SPEAK:Arin
	Don't think we're done lawyering Laura – we're coming back to lawyer twice as hard!
&THINK:Dan
	“Lawyer” isn't a verb, Arin.
&SPEAK:Arin
	Words can be whatever I want ‘em to be, baby! Let's get schmoovin'!
//There are a couple lines omitted from above, it felt like there were two competing punchlines for a single setup and felt clunky. I kept the one that felt like it flowed better.
&THINK:Arin
&SET_POSE:Thinking
	<color="blue>(I wonder if that five dollar footlong I put in the fridge last month is still good; this detective stuff is making me hungry.)

//---------------------------------------[Kitchen: Burgie's Testimony]------------------------------------------//
&FADE_OUT:2
&FADE_OUT_SONG:2.5
&NARRATE
	<color=\#5ee320>[The player moves back to the kitchen.]
&HIDE_ACTOR
&FADE_IN:2
&PLAY_SONG:InvestigationJoonyer,1
&WAIT:2
&SHOW_ACTOR:Burgie
&ACTOR:Burgie
&SPEAK:Burgie
	Ay, you're back.
	
&THINK:Arin
	You're… still here. How long is a union break meant to be again?

&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
&SET_POSE:ThumbsUp
&SPEAK:Burgie
	That's an <color=\#d10a1e>onion break</color> to you, pal, remember? I'm visitin' some fam in the fridge.
&SET_POSE:Thinking
	I also had to throw out this really nasty sub. It was growing a whole ecosystem on it.
	
&ACTOR:Dan
&SPEAK:Dan
	Gross.
	
&THINK:Arin
&PLAY_SFX:Whoops
	I was gonna eat that later…
	

&SET_POSE:Angry
&SHAKE_SCREEN:0.4,0.3
&PLAY_SFX:smack
&SPEAK:Dan
	<color=\#d10a1e>Really gross.</color>
	
&ACTOR:Burgie
&SPEAK:Burgie
&SET_POSE:Sweaty
	Sorry, pal, I didn't know. I'll treat ya to some lunch on payday.
&SET_POSE:ThumbsUp
	How's your bounty huntin' going then, cowboys? Didja speak to the weirdo downstairs?
	
&ACTOR:Dan
&SET_POSE:Angry
&SPEAK:Dan
	Laura says it wasn't her. She's convinced that it had to be you, because she was <color=\#d10a1e>at her desk all day</color>.

&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:realization
&ACTOR:Burgie
&SET_POSE:normal
	Aight. And do <color=\#d10a1e>you</color> think it was me?

&DIALOGUE_SPEED:0.12
&THINK:Arin
&AUTO_SKIP:true
	Uh… 
&AUTO_SKIP:false
&CONTINUE_DIALOGUE
&DIALOGUE_SPEED:0.03
&THINK:Arin
	well, we've got no other leads, so…
&SPEAK:Burgie
	You can't arrest someone on a vague suspicion of commitin' a crime, pal.
	So you better think of summat before I get back to work.

&NARRATE
	<color=\#5ee320>[The Player uses >Talk]

	<color=\#2064e3>...<br><color=\#5ee320>[Hello...]</color><br><color="yellow">[>Tell me...]</color><br><color=\#5ee320>[Can you...]</color>
	<color=\#2064e3>Tell me...</color><br><color="yellow">[who have you seen...]</color><br><color=\#5ee320>[>about...]</color>
	<color=\#2064e3>Tell me, who you've seen...</color><br><color="yellow">[>in the kitchen?]</color><br><color=\#5ee320>[not in the kitchen?]</color>

&ACTOR:Dan
&SPEAK:Dan
	<color=\#5ee320>Tell me, who have you seen in the kitchen?
	
&ACTOR:Burgie
&SPEAK:Burgie
&SET_POSE:Sweaty
	Mm, lemme think…
&SET_POSE:Normal
	Well, I've seen you two moochin' around, o'course. Puttin' your noses in places they don't belong.
	
&ACTOR:Dan
&SHAKE_SCREEN:0.25,0.3
&PLAY_SFX:stab
&SPEAK:Dan
	We're investigating a crime scene. Our noses definitely belong there.
	
&THINK:Arin
	Yeah! It's part of our job description.
	
&ACTOR:Burgie
&SPEAK:Burgie
	Reeeeal suspicious.

&THINK:Arin
	There's nothing suspicious at all!

&ACTOR:Burgie
&SPEAK:Burgie
&SET_POSE:nervous
	And then you got two-eyes downstairs. She was here for a while. Maybe too long…

&ACTOR:Dan
&SPEAK:Dan
	Hey, c'mon, Burgie. Why are you picking on Laura all the time? She's done nothing wrong to you.

&ACTOR:Burgie
&SPEAK:Burgie
&SET_POSE:nervous
	Well, she ain't done nothin' right either.

&SHAKE_SCREEN:0.25,0.3
&PLAY_SFX:realization
&SET_POSE:normal
	Ah, mustardfucker, I'll tell ya who else I saw today! <color=\#d10a1e>Trivia Boy</color>!
	
&ACTOR:Dan
&SPEAK:Dan
&SHAKE_SCREEN:0.35,0.5
&PLAY_SFX:shock2
	<color=\#d10a1e>Trivia Boy's here?!</color>

&ACTOR:Burgie
&SPEAK:Burgie
&SET_POSE:thinking
	No, wait… Trivia Boy's the one with the tie, right?

&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
&ACTOR:Dan
&SPEAK:Dan
	That's <color=\#d10a1e>Tutorial Boy</color>!
	
&ACTOR:Burgie
&SPEAK:Burgie
&SET_POSE:ThumbsUp
	That's the one. Yeah, I saw him go in and out of the kitchen. And then ‘bout <color=\#d10a1e>five minutes later</color>, Laura left.
	Real smug lookin', too.

&ACTOR:Dan
&SPEAK:Dan
&SET_POSE:angry
	In his defense, I think he's kind of stuck looking like that.
	
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:realization
&THINK:Arin
	Weird. You'd have thought Laura would have mentioned him…

&NARRATE
&PLAY_SFX:EvidenceDing
	<color=\#5ee320> ["Burgie's Testimony" Added to Court Record.]

&ACTOR:Dan
&SPEAK:Dan
&SET_POSE:angry
&AUTO_SKIP:true
	You'd have thought 
&CONTINUE_DIALOGUE
&SHAKE_SCREEN:0.35,0.35
&PLAY_SFX:stab
	<color=\#d10a1e>Burgie</color> 
&CONTINUE_DIALOGUE
&AUTO_SKIP:false
	might've mentioned him earlier, too.

&ACTOR:Burgie
&SPEAK:Burgie
	I saw Laura enter and assumed that was your thief. Ya know – last one in the room committed the crime?

&ACTOR:Dan
&SPEAK:Dan
&SET_POSE:angry
&SHAKE_SCREEN:0.35,0.35
&PLAY_SFX:stab2
	You still could have <color=\#d10a1e>mentioned</color> him!

&ACTOR:Burgie
&SPEAK:Burgie
&SHAKE_SCREEN:0.45,0.35
&PLAY_SFX:stab2
	Give a burger some slack! Memory ain't my strong suit, alright?! I don't even know if I got a brain!

&THINK:Arin
	You know, I've wondered that, but I thought it was too insensitive to bring up…
	
&ACTOR:Dan
&SPEAK:Dan
&SET_POSE:angry
	As much as an enigma Burgie's very existence is, we have more pressing matters to think about right now.

&THINK:Arin
	Yeah, something's <color=\#d10a1e>fishy</color>, and it's not that leftover salmon from last Tuesday. We need to talk to <color=\#d10a1e>Laura</color> again.

//----------------------------------------[Gauntlet 3: The final lock]---------------------------------------//

&FADE_OUT:1
&NARRATE
	<color=\#5ee320>[Player moves back to the reception]
&HIDE_ACTOR
&SCENE:Reception
&PLAY_SONG:InvestigationUniCore,1
&FADE_IN:2
&SHOW_ACTOR
&ACTOR:Laura
&SPEAK:Laura
	Welcome back, boooooys. I was just drafting up a treatment for my next shoooow.
	‘Laura Schmiiiiitt's Expedition to Mount Wycheproooooof'. I might expand into documentarieeeees.

&SHAKE_SCREEN:0.35,0.35
&PLAY_SFX:damage2
&THINK:Arin
	Enough of your distractions! We're back to lawyer twice as hard as my dick is right now!
	
&ACTOR:Dan
&SPEAK:Dan
	Hell yeah, that's the attitude I like.
	
&NARRATE
	<color=\#5ee320>[Player can now use the Investigation Menu]
&NARRATE
	<color=\#5ee320>[Player uses >Talk with Laura and resumes Seductive Reasoning]
&ACTOR:Dan
&NARRATE
&PLAY_SFX:lightbulb
	<color="yellow">[>Tell me, who was really in the kitchen with you?]</color><br><color=\#5ee320>[Hello...]</color><br><color=\#5ee320>[Tell me...]</color><color=\#5ee320><br>[Can you...]</color>
&SPEAK:Dan
	<color=\#5ee320>Tell me, who was really in the kitchen with you?
&PLAY_SONG:FyiIWannaXYourExaminationAllegro,1
&ACTOR:Laura
&SPEAK:Laura
&SET_POSE:sweaty
	Oh my Gaaaawd. This is the part where the good cops become bad cops… exciiiiiting.
&SET_POSE:nervous
	If it wasn't that wallaby Roooooss, then it was definitely Burgieeeee. He's the only one leeeeeft.

&NARRATE
&AUTO_SKIP:true
	<color=\#5ee320>[Present:"Burgie's Testimony"]<br>
&CONTINUE_DIALOGUE
	<color=\#5ee320>[Burgie thought you were the thief!]<br>
&CONTINUE_DIALOGUE
	<color="yellow">[>Tutorial Boy is also here!]<br>
&CONTINUE_DIALOGUE
&AUTO_SKIP:false
	<color=\#5ee320>[Burgie can't eat chicken nuggets!]

&SHAKE_SCREEN:0.35,0.35
&PLAY_SFX:TakeThatArin
&SET_POSE:sweaty
&THINK:Dan
	Burgie testified that Tutorial Boy is here today, too! You'd think <color=\#d10a1e>as a receptionist</color>, you'd keep track of HIM being around!

&SHAKE_SCREEN:0.45,0.35
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:shock2
&THINK:Dan
	If it wasn't anybody else in the office… it <color=\#d10a1e>MUST have been Tutorial Boy</color>!
	
&SHAKE_SCREEN:0.45,0.35
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:stab2
&SPEAK:Laura
	Urp!
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
	Yikes!!
	
&THINK:Dan	
	We got her on tenterhooks Arin!
	
&SPEAK:Laura
&SET_POSE:nervous
	W-Well, yeeeeah… he should have signed iiiiiin…
&SHAKE_SCREEN:0.45,0.35
&PLAY_SFX:smack
	B-But just because Buuuurgie saw him, doesn't mean I diiiiid! <color=\#d10a1e>He must've snuck into the kitcheeeeen after I leeeeeft</color>!
&SET_POSE:thinking
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:realization
	He's also got a really suspicious aaaaair about hiiiim…

&SET_POSE:normal
&THINK:Dan
	You seem to think that about everybody.
	
&SPEAK:Laura
&SET_POSE:ThumbsUp
	I'm not suspiciouuuus, I'm <color=\#d10a1e>mysteriooooous</color>. Big sexy difference, Daaaan.

&SHAKE_SCREEN:0.35,0.25
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:damage2
&THINK:Dan
	I'm the only qualified guy who gets to decide what's sexy and what's fuckin' weird, okay?!
	
&ACTOR:Arin
&SPEAK:Arin
	She's trying to bewitch you with her non-mysterious ways, Dan! She's lying again! Show her the evidence!
	
&NARRATE
&AUTO_SKIP:true
	<color=\#5ee320>[Present:"Burgie's Testimony"]<br>
&CONTINUE_DIALOGUE
	<color=\#5ee320>[Dan's the first person Burgie saw!]<br>
&CONTINUE_DIALOGUE
	<color=\#5ee320>[The nuggets became sentient and ran off!]<br>
&CONTINUE_DIALOGUE
&AUTO_SKIP:false
	<color="yellow">[>Laura was the last person Burgie saw!]
	
&ACTOR:Laura
&SET_POSE:nervous
&SHAKE_SCREEN:0.35,0.35
&PLAY_SFX:smack
&THINK:Dan
	According to Burgie's testimony, when you entered the kitchen… <color=\#d10a1e>Tutorial Boy came in later</color>!
&SHAKE_SCREEN:0.35,0.35
&PLAY_SFX:damage1
&THINK:Dan
	And when he left the kitchen, you left <color=\#d10a1e>only five minutes after</color>!
&SHAKE_SCREEN:0.35,0.25
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:shock2
&PLAY_SONG:DragonObjection,1
&THINK:Dan
	That means it was IMPOSSIBLE for you to NOT see him! You either <color=\#d10a1e>saw</color> the culprit… or <color=\#d10a1e>you ARE the culprit</color>!

&SHAKE_SCREEN:0.35,0.25
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:stab2
&ACTOR:Laura
&SET_POSE:sweaty
&SPEAK:Laura
	Eep!
	How could you do this to a poor, small-town girl, boooooys?

&SHAKE_SCREEN:0.35,0.25
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:damage2
&THINK:Dan
	You better spill the beans, Laura! Was it <color=\#d10a1e>you</color>, or was it <color=\#d10a1e>Tutorial Boy</color>?!
	
&SPEAK:Laura
	I'm not the…!
	...
	… gulp.
&SET_POSE:ThumbsUp
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
&STOP_SONG
	Okaaaay boooys, you caught meeee. Good job, ahahaaaa…
	
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:realization
&ACTOR:Arin
&SPEAK:Arin
&SET_POSE:ShockAnimation
	Huh?
&APPEAR_INSTANTLY
//prevents text from advancing after the animation completes
	Huh?
&ACTOR:Laura
&SET_POSE:ThumbsUp
&PLAY_SFX:lightbulb
&PLAY_SONG:ObjectsOfVictory,1
&SPEAK:Laura
	I'm the culpriiiiit. I ate the nuggeeeets. I'm a hungry gaaaal, what can I saaaay?
	
&THINK:Dan
&ACTOR:Arin
&SET_POSE:thinking
	Finally, we got 'em! And after that wild goose chase all afternoon…
&DIALOGUE_SPEED:0.5
&AUTO_SKIP:true
	...
&CONTINUE_DIALOGUE
&AUTO_SKIP:false
&DIALOGUE_SPEED:0.03
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
&THINK:Dan
&FADE_OUT_SONG:2
	Arin, what's up? You don't look satisfied.

&SET_POSE:normal
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
&AUTO_SKIP:true
&SPEAK:Arin
	Huh… 
&CONTINUE_DIALOGUE
&WAIT:0.75
	well,
&CONTINUE_DIALOGUE
&WAIT:0.6
&AUTO_SKIP:false
&SET_POSE:Confident
	that's because…

&ACTOR:Laura
&SET_POSE:ThumbsUp
&WAIT:1.5
&SHAKE_SCREEN:0.5,0.3
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:DramaPound
&PLAY_SONG:StarlightObjection,0
&SET_POSE:sweaty
&THINK:Arin
	She's <color=\#d10a1e>STILL LYING</color>!

&SHAKE_SCREEN:0.35,0.25
&PLAY_SFX:shock2
&THINK:Dan
	…!
&SHAKE_SCREEN:0.35,0.25
&PLAY_SFX:stab
&SPEAK:Laura
	Wah!
&ACTOR:Arin
&SET_POSE:Confident
&SHAKE_SCREEN:0.35,0.25
&PLAY_ANIMATION:Blink
&SCENE:Reception
&THINK:Dan
	But Arin, <color=\#d10a1e>she confessed</color>!
	
&SPEAK:Arin
	You think Laura will just admit to a crime so heinous after defending herself for so long!?
&SHAKE_SCREEN:0.5,0.3
&PLAY_ANIMATION:Blink
&SCENE:Reception
&SET_POSE:Point
&PLAY_SFX:shock2
	No way, Joseph!
	
//rewritten slightly to put the objection in Dan's responsibility
&PLAY_SFX:realization
&THINK:Dan
&AUTO_SKIP:true
	But the only reason Laura would lie would be
&CONTINUE_DIALOGUE
&DIALOGUE_SPEED:0.2
...
&WAIT:1
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
!
&DIALOGUE_SPEED:0.03
&CONTINUE_DIALOGUE
&THINK:Dan
<br> Arin, you're right! I think see where you're going.

&PLAY_SFX:realization
&SET_POSE:thinking
&SPEAK:Arin
	Wait, really? I was just guessing.
	
&THINK:Dan
	Then I'm about to blow your freakin mind! Sit tight big cat, and watch this...
&ACTOR:Laura
&SET_POSE:Nervous
&THINK:Dan
	Laura… not only am I gonna prove that you <color=\#d10a1e>didn't</color> commit the crime…
&SHAKE_SCREEN:0.5,0.3
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:damage2
&THINK:Dan
	But also prove that it was <color=\#d10a1e>Tutorial Boy</color> who did it!

&SHAKE_SCREEN:0.5,0.3
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:stab2
&SET_POSE:sweaty
&SPEAK:Laura
	!!!
&SET_POSE:nervous
	Y-You don't have to do that, Daaaan… I'll come quietlyyyy…

&SHAKE_SCREEN:0.5,0.3
&PLAY_SFX:smack
&THINK:Dan
	Don't interrupt my awesome shining moment, Laura!
	See, if you were really the food thief… you would have used something that Tutorial Boy <color=\#d10a1e>couldn't even consider</color>!

&NARRATE
&AUTO_SKIP:true
	<color=\#5ee320>[Present:"Unused Cutlery"]<br>
&CONTINUE_DIALOGUE
	<color="yellow">[>For safely handeling nuggets as they cool!]<br>
&CONTINUE_DIALOGUE
	<color=\#5ee320>[The crumbs look like Tutorial Boy's head!]<br>
&CONTINUE_DIALOGUE
&AUTO_SKIP:false
	<color=\#5ee320>[The cutlery is missing a spoon!]
	
&AUTO_SKIP:true
	<color=\#5ee320>[Also Present:"Nugget Packaging"]<br>
&CONTINUE_DIALOGUE
	<color=\#5ee320>[No human could eat twelve of these!]<br>
&CONTINUE_DIALOGUE
	<color="yellow">[>The food is extremely hot after cooking!]<br>
&CONTINUE_DIALOGUE
&AUTO_SKIP:false
	<color=\#5ee320>[They're limited edition unique dino shapes]
	
&THINK:Dan
	I noticed that the cutlery was unused, because it was completely clean and <color=\#d10a1e>still wrapped</color> in a napkin…
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
&THINK:Dan
	I thought it was strange, because the packaging for the nuggets clearly states <color=\#d10a1e>they're extremely hot once cooked</color>! 
&SET_POSE:sweaty
&THINK:Dan
	It's why I left them alone in the first place!
&THINK:Dan
	I tried to eat them straight out of the oven once...
&SET_POSE:nervous
&THINK:Dan
&CONTINUE_DIALOGUE
	Never again. Scarred for life, physically and emotionally.	
&SET_POSE:sweaty
&THINK:Dan
	So if the criminal ate them without utensils, they'd have pretty <color=\#d10a1e>bad burns</color>…
&SHAKE_SCREEN:0.5,0.3
&PLAY_SFX:smack
&THINK:Dan
	And I don't see any on YOU, Laura!
	
&SHAKE_SCREEN:0.5,0.3
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:stab2
&SPEAK:Laura
	I-I… well… 
		
&SHAKE_SCREEN:0.5,0.3
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:damage1
&THINK:Dan
	Any dastardly devil would have used the cutlery that was conveniently set aside next to the microwave, <color=\#d10a1e>except one</color>…
&SHAKE_SCREEN:0.5,0.3
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:DramaPound
&THINK:Dan
	Tutorial Boy, <color=\#d10a1e>who doesn't have arms</color>!!
	
&NARRATE
	<color=\#5ee320>[The final lock shatters, and so does Laura's resolve to lie any longer]<br>
	
&SHAKE_SCREEN:0.5,0.3
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:stab2
&SPEAK:Laura
	Gasp! Shock! Horroooooor!
&PLAY_SONG:TutorialBoysTragicallyGenericReminiscence,2
&SET_POSE:nervous
	I guess this is the part where the criminal tells the <color=\#d10a1e>reeeeal truuuuth</color>…
&SET_POSE:ThumbsUp
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
	Aaaaah, this is so excitiiiiiing…!
	
&SHAKE_SCREEN:0.3,0.3
&PLAY_SFX:smack
&SET_POSE:nervous
&THINK:Dan
	Quit stalling and start talking! 
&SHAKE_SCREEN:0.45,0.2
&PLAY_SFX:stab
&AUTO_SKIP:true
&THINK:Dan
	Where are the dinos, Laura?! 
&WAIT:0.5
&SHAKE_SCREEN:0.8,0.75
&SET_POSE:sweaty
&PLAY_SFX:stab2
&CONTINUE_DIALOGUE
&THINK:Dan
&AUTO_SKIP:false
	WHERE ARE THEY?!


&SPEAK:Laura
&SET_POSE:nervous
	It wasn't me who stole the nuggeeeets… it was <color=\#d10a1e>Tutorial Booooy</color>.
	And I saw the whoooole thiiiing.
	
&SHAKE_SCREEN:0.3,0.3
&PLAY_SFX:smack
&PLAY_ANIMATION:Blink
&SCENE:Reception
&ACTOR:Arin
&SET_POSE:Annoyed
&SPEAK:Arin
	You saw the <color=\#d10a1e>whole thing</color> and you didn't tell us?! Why did you cover it up?

&THINK:Dan
&PLAY_SFX:realization
	Isn't it obvious, Arin?
&THINK:Dan
&SET_POSE:Thinking
	They're in love! This is a <color=\#d10a1e>romance</color> story!
&PLAY_ANIMATION:Blink
&SCENE:Reception
&SET_POSE:sweaty
&PLAY_SFX:lightbulb
&SPEAK:Arin
	Huuuuuh?!

&THINK:Dan
	Laura has <color=\#d10a1e>a huge crush</color> on Tutorial Boy… so when she saw him stealing the nuggets, she had to <color=\#d10a1e>defend his honor</color>!
&ACTOR:Laura
&SET_POSE:sweaty
&PLAY_SFX:lightbulb
	That's so sweet, Laura!

&SET_POSE:ThumbsUp
&SPEAK:Laura
	That is soooo sweeeeet. I have to write it down for my next skeeeetch.
&SET_POSE:nervous
	It's also <color=\#d10a1e>not truuuuuue</color>.
	
&THINK:Dan
&AUTO_SKIP:True
	Oh. 
&WAIT:0.6
&CONTINUE_DIALOGUE
&AUTO_SKIP:false	
	Nuts.

&SET_POSE:sweaty
&SPEAK:Laura
&AUTO_SKIP:true
	The truth iiiiis… Tutorial Boy 
&CONTINUE_DIALOGUE
&AUTO_SKIP:false	
&PLAY_SFX:realization	
&PLAY_ANIMATION:Blink
&SCENE:Reception
	<color=\#d10a1e>blackmailed</color> meeeee.
	When he came into the kitchen, h-he… he saw me…
	
&ACTOR:Arin
&SPEAK:Arin
	It's okay Laura, this is a safe space! You can tell us any–

&SHAKE_SCREEN:0.5,0.3
&PLAY_SFX:DramaPound
&PLAY_ANIMATION:Blink
&SCENE:Reception
&ACTOR:Laura
&SET_POSE:nervous
&SPEAK:Laura
	I was eating cereal with pilk and ice-cream!
	
&SHAKE_SCREEN:0.3,0.3
&PLAY_ANIMATION:Blink
&SCENE:Reception
&ACTOR:Arin
&SET_POSE:ShockAnimation
&PLAY_SFX:Shock2
&STOP_SONG
&SPEAK:Arin
	…!
//cancel the animation transition
&APPEAR_INSTANTLY
	…!
&SHAKE_SCREEN:0.6,0.35
&PLAY_SFX:damage2
&THINK:Dan
	You were eating <color=\#d10a1e>WHAT</color>?
	
&PLAY_SONG:TutorialBoysTragicallyGenericReminiscence,1
&ACTOR:Laura
&SPEAK:Laura
&SET_POSE:sweaty
	Whole-wheat cereaaaaal. With pilk. And mint ice-creeeeam. 
&SET_POSE:nervous
	And yeeees, the ice-cream was a toppiiiiing, not a siiiide.
	
&ACTOR:Arin
&SET_POSE:Thinking
&SPEAK:Arin
	The hell is a pilk?

&ACTOR:Laura
&SPEAK:Laura
&SET_POSE:nervous
	Pepsi and Miiiiilk. It's a countryside-big city combooooo. Veeery populaaaar.
	
&PLAY_SFX:Whoops
&THINK:Dan
	I <color=\#d10a1e>really wish</color> it was the love story now…
	
&ACTOR:Laura
&SPEAK:Laura
&SET_POSE:nervous
	I saw Tutorial Boy take your nuggets and eat theeeeem… it wasn't very seeexyyyy. //changed line from "hot" to "sexy" to not confuse the player about whether tutorial boy isn't hot, or the nuggets weren't
	He just <color=\#d10a1e>faceplanted right into theeeem</color>. And chewed veeeery louuuudly. 
&SET_POSE:normal
	Innnnnnnstant turn ooooooff. //Changed the repeated letter to n instead of i since the capitalization made it look off.
	
&ACTOR:Arin
&SET_POSE:Sweaty
&SPEAK:Arin
	Yeah, there's a reason why we <color=\#d10a1e>uninvited</color> him to company lunches.
	
&ACTOR:Laura
&SPEAK:Laura
&SET_POSE:nervous
	I tried to tell him that the nuggets weren't hiiiis to eaaaat. B-But…
&PLAY_SFX:Whoops
	H-He made fun of meeee… for myyy luuuunch…
	He said if I told you twoooo that he stole your foooood… he'd tell EVERYONE about my cereal and ice-creeeeam…

&THINK:Dan
	Look, it'd be excusable if you dropped the pepsi and changed to vanilla or something!
	
&SPEAK:Laura
	The best part is that it's only five thousaaand calorieeeees.
	
&SHAKE_SCREEN:0.3,0.3
&PLAY_SFX:damage1
&THINK:Dan
	That's easily in my top five weirdest foods I've heard of. And I've watched Arin <color=\#d10a1e>chug M&M's</color> out of a jar.
	
&ACTOR:Arin
&SET_POSE:Confident
&PLAY_SFX:lightbulb
&SPEAK:Arin
	Hey, don't knock it before you try it.
	
&ACTOR:Laura
&SPEAK:Laura
&SET_POSE:normal
	Same goes for my luuuuuunch. Speaking oooooff… my <color=\#d10a1e>pineapple and marshmallow boba smoothie</color> is heeeeree. Be right baaaack.
&HIDE_ACTOR
&PLAY_SONG:ObjectsOfVictory,5
&WAIT:2
&SHOW_ACTOR
&ACTOR:Dan
&SET_POSE:Angry
&SPEAK:Dan
	Oh yeah, <color=\#d10a1e>speaking of lunch</color> - we better go chase down the guy who actually did this! 
&PLAY_ANIMATION:Blink
&SCENE:Reception
&PLAY_SFX:lightbulb
	Now we've got Burgie's testimony and Laura as an eyewitness, he won't be able to wriggle out of it!
&SET_POSE:Normal
	Thanks for your help, Arin. I knew I could rely on you!
	
&THINK:Arin
	What can I say? I'm the best lawyer this side of town. Nothing gets past me.
	
&SET_POSE:Angry
&SHAKE_SCREEN:0.3,0.3
&PLAY_SFX:smack
&SPEAK:Dan
	Okay okay, quit stroking your dick. They were only nuggets.
&SHAKE_SCREEN:0.3,0.3
&PLAY_SFX:damage1
&PLAY_ANIMATION:Blink
&SCENE:Reception
	Now let's go get Tutorial Boy's ass for stealing my damn food!
	
&ACTOR:Arin
&SET_POSE:Confident
&SPEAK:Arin 
	And deliver some sweet joint–

&SHAKE_SCREEN:0.3,0.3	
&ACTOR:Dan
&SET_POSE:Angry
&SPEAK:Dan
&PLAY_SFX:shock2
&AUTO_SKIP:true
&DIALOGUE_SPEED:0.03
	Don't you dare finish that line. We are NOT finishing this the same way as–––
&AUTO_SKIP:false
&SHAKE_SCREEN:0.3,0.3
&PLAY_SFX:ObjectionArin
&PLAY_ANIMATION:Blink
&SCENE:Reception
&ACTOR:Arin
&SET_POSE:Objection
&SPEAK:Arin 
	SOME SWEET JOINT JUSTICE!
	
&THINK:Dan
	SON OF A–
	
&FADE_OUT:5
&FADE_OUT_SONG:8
&NARRATE
	<color=\#5ee320> [End.]

-> DONE