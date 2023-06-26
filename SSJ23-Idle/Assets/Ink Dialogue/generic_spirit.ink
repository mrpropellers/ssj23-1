VAR currentStoryKnot = "greet"

{~Hello|Greetings|Good evening|What nice night|Hi}{~!|!|.|.|...|?} {~I do like this tree.|I dig the energy you're putting out.|This seems like a great place to spend some time!|This is cool. Like really cool.|What a lovely spot.|I bet a bunch of spirits will be here soon!} {~I'm going to stay here awhile!|Don't mind if I chill for a bit? Excellent!|This tree will make a great place to hole up for awhile. Is it okay if I stay?|May I please stay here?|How about I stay here?|I'd love to stay! Is that okay?}
        *[{~Of course!|Absolutely.|You're welcome to stay.|The tree is for all spirits.|I'm happy to share.|Thank you for being here.|Enjoy the tree!}]
    {~Thank you!|Thanks!|I appreciate it.|I'm much obliged.|Thank you so much!|Marvelous!|I appreciate you.|Thank ya!}
~ currentStoryKnot = "about"
    ->END
===about===
<-randomHello
{~<-randomEncouragement|<-randomEncouragement|<-randomAboutMe|<-randomTreeFact|<-randomAdvice|<-randomAdvice}
//this is going to need a stopping point built in lol
->END

===randomHello===
{~How are you tonight?|I bet you got so much done today{~!|.|!!}|Hello!|Oh, hi! {~I didn't see you there!||What year is it?|The tree grew again!}|The weather is perfect tonight!|Every time I wake up here I'm so happy.|Good evening!|Hello again!|Hey! How was your day?|It's good to be here again!}

{~Oh good!||Fantastic!|What's this?|Excellent!|Look at you!|Did your work go well today?|Well isn't this something!|Do my eyes deceive me?|Yay!|What's going on?|Unless I'm mistaken...|Wow, take a look at this!} {~You're finished for now?|Is it time for you to rest?|Are you taking a break?|You're able to stop for awhile?|It's time for a pause.|You're all done for a moment.|You've got a bit of time for you!}

->DONE

===randomAboutMe===
{~<-i_like|<-i_dislike}
->DONE
=i_like
 {~{~My favorite thing is|I love|I really love|I can't get enough of|I deeply appreciate} {~when the leaves line up in a perfect triangle.|how the moonlight makes the bark look.|how this room is laid out.|the time of night right before everyone else reappears.|getting to sleep early. I need more {~rest|sleep|beauty sleep|quiet time} than most other spirits.}|I had the strangest dream, but I forgot it.}
 ->DONE
 =i_dislike
 I {~kind of |totally |mildly | | |kind of |wildly |slightly }{~hate|despise|can't stand|dislike|loath|hate|hate|avoid} {~{~getting|waking|staying} up {~too|super|very} {~early|late}.|{~anybody|anyone|spirits} who {~don't make time for their friends|can never decide what they want|always need help but don't return the favor|borrow books and don't return them|only complain about things|don't contribute to our group effort|seem unable to put positive energy in the world|always finish my sentences|are always late|are always super early|always bring up embarassing old memories|never host but always invite themselves over}{~!|.|.|...} {~Do you know what I mean?|Does that make sense?|Is that fair?|Oh no... Am I doing that right now?|Although sometimes I worry I'm projecting.|It just drives me crazy!|Ugh!|Thanks for letting me vent.|I appreciate you always listening.|It doesn't happen to me often but I guess it's particularly frustrating to me.|I just... Well... Gah! Nevermind.}}
 ->DONE

===randomTreeFact===
{~Here's a random tree fact for you!|I've got one you might not know!|Can I tell you something about trees like this?|Listen to this!|Hey! Listen!|I learned something I think you might like to know!} 
{~Technically,|Obviously,|Apparently,|So,|I guess|I heard|It turns out that} {~the biggest bonsai tree in the world is sixteen feet tall!|while this tree gets everything it needs from your work, real bonsai trees need to be carefully watered and have their soil monitored.|bonsai trees have been cultivated for thousands of years!|the {~largest|biggest|most enormous} {~display|showcase|group collection} of bonsai trees was in India and featured 2,649 trees!|there are actually many types of bonsai trees, but this one is its own special variant.|the smallest bonsai trees would easily fit in the palm of your hand.|the most expensive bonsai ever sold was for over a million dollars!|The word 'bonsai' means "tree in flowerpot."|there are some bonsai trees that are over a thousand years old.|Bonsai trees are some of the only trees that aren't planted for their fruit, bark or to repopulate forests. They're planted just for decoration and apprecation!}
->DONE

===randomAdvice===
{~Don't forget to drink some water. Your work may hydrate the tree, but it doesn't hydrate you!|Take a deep breath! Think about something that makes you happy!|Maybe take a walk during your next break? Moving around helps me, maybe it will help you!|Make sure you're setting reasonable goals and treating yourself when you accomplish them!|I know it sounds silly, but it always helps me to take a moment and say, "I'm proud of myself!" Say it out loud! It will help!|Stand up and stretch! Your physical form helps your mental form!|Do you need a snack? I {~love|like|always need|can't get enough|am always motivated by} snacks.|If I could send a message to my best friend, I would right now! Spirits can't always do that, so maybe send one to your friend for me?|Send someone a nice message on your break. It always makes me feel better!|Don't think about work while you're resting! It will be there when you get back.}
->DONE

===randomEncouragement===
{~It's incredible to me that you can stay focused for so long! Even if time works differently for us than it does for you, it's still an accomplishment.|Every time I wake up the tree has been nourished by your focus!You're doing a fantastic job! I hope you can take a moment to appreciate yourself.|Please don't forget to take a moment to encourage yourself!|If you keep this up, you're bound to succeed.|I honestly am so impressed by your dedication.|Way to go! It's inspiring to wake up here and see how much you've done!|Don't sell yourself short! Every bit of progress adds up!|I just wanted to say I really appreciate what you're doing. It's not always easy to keep going, but every time you do you get better at it!Stick with it!Every time you keep going you get closer to the finish line.|If you don't mind me saying, I'm quite proud of you. Both small steps and big steps are worth taking a moment to appreciate!}
->DONE