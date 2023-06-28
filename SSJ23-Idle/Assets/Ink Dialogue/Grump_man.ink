VAR currentStoryKnot = "Greet"

I could sense how industrious you were being and had to see for myself.
I'll make it a goal to be here every sunset and check on your progress.
~ currentStoryKnot = "Tip1"

VAR isTutorial = "False"
INCLUDE UniversalTutorial.Ink
{ isTutorial == "True":
~ isTutorial = "False"
->Tutorial->
}
->DONE

===About1===
->Greeting5->

I admire people who work hard. I've always enjoyed getting things done.
~ currentStoryKnot = "Tip3"
->DONE


===About2===
->Greeting9->

I often think of home. Traveling is important to stay young, but I always feel like my best self at home.
~ currentStoryKnot = "About3"
->DONE


===About3===
->Greeting10->

Not every human works has hard as you do. 
Although some work harder.
Either way, what everyone else does isn't that important.
~ currentStoryKnot = "Encourage4"
->DONE


===About4===
->Greeting13->

Time works differently for spirits. 
For me, it looks like you work from sun up to sundown.
~ currentStoryKnot = "Encourage5"
->DONE


===About5===
->Greeting15->

I can only relax when I feel like I've earned it.
You're certainly earning it!
~ currentStoryKnot = "Convo3"
->DONE


===Encourage1===
->Greeting2->

You've got your work cut out for you, but I would bet you'll do a great job.
~ currentStoryKnot = "Tip2"
->DONE


===Encourage2===
->Greeting4->

I wouldn't say I'm surprised, but you're really pulling it together.
~ currentStoryKnot = "About1"
->DONE


===Encourage3===
->Greeting8->

Some days are easier than others. That's fine.
Don't focus on the day. Focus on yourself. You can't control the day, but you can control how you handle it.
~ currentStoryKnot = "About2"
->DONE


===Encourage4===
->Greeting11->

A fine job. Your hard work is evident.
~ currentStoryKnot = "Convo2"
->DONE


===Encourage5===
->Greeting14->

I'm proud of you. You should be proud of yourself.
~ currentStoryKnot = "About5"
->DONE


===Tip1===
->Greeting1->

Branches that grow near the base of tree are called, "suckers."
Trim them so they don't drain your growth!
~ currentStoryKnot = "Encourage1"
->DONE


===Tip2===
->Greeting3->

Branches will bud from points in the tree called, "Nodes." If a node is too crowded, it'll slow the growth of other branches.
~ currentStoryKnot = "Encourage2"
->DONE


===Tip3===
->Greeting6->

Pruning should be relaxing. Treat it like what it is: an opportunity to focus completely on somebody else.
~ currentStoryKnot = "Convo1"
->DONE


===Convo1===
->Greeting7->
Tree Keeper, {~May I ask you something?,Do you have a minute to talk?,Would you have a minute to discuss something?|I'd be grateful for a minute of your time.|Have a second to chat?}
    *[Some other time.]
        Of course! Enjoy the evening!
        ~currentStoryKnot = Greeting7
        ->DONE
    *[Sure!]
        Humans and spirits lead very different lives. In your mind, among humans, what is a friend?
        *[Someone who is there for you.]
            That makes sense.
        *[Someone you'd be there for.]
            That's noble of you. I suppose it goes both ways.
        - Spirits have very few needs, so it's easier for us to be independent. 
            Some spirits live many, many years without interacting with anyone.
            I don't know of many humans who live like that.
            Either way, I see how often you've been here for this tree. 
            You're a friend to it, in away.
            And therefore, to all of us.
            Thank you.
~ currentStoryKnot = "Encourage3"
->DONE


===Convo2===
->Greeting12->
When we talked about what it means to be a friend, I realized something.
Spirits don't have many needs, but there's always something else to want.
A greedy spirit can become dangerous. I try not to ask for much, but I worry that I'm missing out on things.
Does that make sense?
    *[You can always ask for help.]
        Do you like it when people ask you for help?
            **[When I can help, yes.]
                I tend to agree. Helping others always feels good.
                If others also like helping, then maybe I'm helping them by asking them for help.
            **[If they don't demand too much.]
                Of course. I suppose there's some decorum inherent to asking for help responsibly.
            **[Always. Helping is great.]
                My thoughts are similar. I wonder if most poeple like helping others?
                 If others also like helping, then maybe I'm helping them by asking them for help.
                - - 
    *[It's good to not be a burden.]
        That's true...
        But I do wonder if there's a line between being a burden and being a friend. 
        It seems to make you happy to help the tree. Does that make the tree a burden?
        Giving things to others makes me happy. I wish they'd ask me more often.
        I can always say no.
    - Anyway, just something that was on my mind. Thank you for the conversation!
~ currentStoryKnot = "About4"
->DONE


===Convo3===
->Greeting16->
    My friend... The tree grows and grows. Soon it will be stronger enough to return us home.
    There are certain places in your world that are attract spirits such as myself.
    You've probably noticed them in your time - places that have a sense of specialness.
        *[Like theatres and auditoriums?]
            I suppose so, yes. Those places can be very special. 
        *[Like memorials or graveyards?]
            Indeed. Though they are somber, they are special.
        *[Like temples or  churches?]
            Absolutely. Sacred places often have this feeling.
        -Places where people feel strongly, or where they devote themselves fully to their emotions.
        It may sound silly, but the time you've devoted to this little tree is not dissimilar. That's why we're here.
        Your effort. Your attention. These things can make anything special. 
        I hope you always have something to devote yourself  to - whether it's your work, your family, your friends... 
        People and places are special because we believe them to be so.
        Thank you for makin this place special to us.
->DONE



===Greeting1===
Good evening, Tree Keeper. Looks like it was a fine day.
->->
->DONE

===Greeting2===
Tree Keeper! It's good to see you.
->->
->DONE

===Greeting3===
Hello again, Tree Keeper.
->->
->DONE

===Greeting4===
Excellent to see you, Tree Keeper.
->->
->DONE

===Greeting5===
Titles may be a bit formal, but I listen to the old ways. Hello, Tree Keeper!
->->
->DONE

===Greeting6===
How are you, Tree Keeper?
->->
->DONE

===Greeting7===
Good evening, Tree Keeper. Hope your day was productive.
->->
->DONE

===Greeting8===
Hello, Tree Keeper... A fine day, was it?
->->
->DONE

===Greeting9===
Another day, another task accomplished. Right, friend?
->->
->DONE

===Greeting10===
Tree Keeper! Good evening!
->->
->DONE

===Greeting11===
The days grow short, my friend. I appreciate each one we get together.
->->
->DONE

===Greeting12===
Good evening, Tree Keeper.
->->
->DONE

===Greeting13===
A fine evening is in the works, wouldn't you say, Tree Keeper!
->->
->DONE

===Greeting14===
My friend! It's good to see you.
->->
->DONE

===Greeting15===
Good evening, friend. How was the day?
->->
->DONE

===Greeting16===
Hello again, my friend.
->->
->DONE