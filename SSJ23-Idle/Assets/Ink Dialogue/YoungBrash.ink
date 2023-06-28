VAR currentStoryKnot = "Greet"

Whoa! This is clearly the place to be. 
I'm not going anywhere! Come find me when the sun sets again.
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

I like to move around all the time! 
But don't worry, I'll be here for a bit.
~ currentStoryKnot = "Tip3"
->DONE


===About2===
->Greeting9->

I like to slide down the trunk of the tree and dodge as many branches as I can! 
Keep growing this tree, and we'll have the best slide ever!
~ currentStoryKnot = "About3"
->DONE


===About3===
->Greeting10->

My ideas need to rattle around in my head until they're ready to come out.
It's not procrastinating! 
It's preparation.
Honest.
~ currentStoryKnot = "Encourage4"
->DONE


===About4===
->Greeting13->

This is my third trip to the human world! Most spirits only go a handful of times.
~ currentStoryKnot = "Encourage5"
->DONE


===About5===
->Greeting15->

When I get crazy ideas, I have to find someone to talk to about them.
~ currentStoryKnot = "Convo3"
->DONE


===Encourage1===
->Greeting2->

Don't hesitate! You can do this!
~ currentStoryKnot = "Tip2"
->DONE


===Encourage2===
->Greeting4->

It is obvious to every spirit here that you're the best. 
Even if they don't say it.
~ currentStoryKnot = "About1"
->DONE


===Encourage3===
->Greeting8->

I've never doubted you for a second!
And anyone that has was wrong. And I'm not just saying that because we're friends.
~ currentStoryKnot = "About2"
->DONE


===Encourage4===
->Greeting11->

Nobody does it how you do it, and that's awesome. I like you for being you. 
And it should be weird for people to say that to each other!
~ currentStoryKnot = "Convo2"
->DONE


===Encourage5===
->Greeting14->

Listen. You've done so much here. I hope you carry that with you.
If things seem to tough, look back on this and remember how awesome you are.
~ currentStoryKnot = "About5"
->DONE


===Tip1===
->Greeting1->

I would prune any and every branch I think is ugly! It's your tree, make it what you want!
~ currentStoryKnot = "Encourage1"
->DONE


===Tip2===
->Greeting3->

In my opinion, your trunk should be the only part of the tree that grows toward the sky! 
~ currentStoryKnot = "Encourage2"
->DONE


===Tip3===
->Greeting6->

It's okay to make mistakes. I make mistakes all the time.
Making a mistake while pruning can be scary because you can't take it back.
But there's lots of stuff in life like that - you just have to trust yourself and do better next time.
~ currentStoryKnot = "Convo1"
->DONE


===Convo1===
->Greeting7->

We're not supposed to talk too much about being a spirit.
But in my mind seeing what you would ask us about is actually learning more about humans! 
->Spirit_Questions
=Spirit_Questions
{!So what would you want to know?|What else?|Anything else?}
    *[Where do spirits live?]
        Everywhere! ->Spirit_Questions
    *[What do spirits do?]
        Anything we want to do! ->Spirit_Questions
    *[How long do spirits live?]
        As long as we like! ->Spirit_Questions
    *->
    -Good talk! Thanks for that!
~ currentStoryKnot = "Encourage3"
->DONE


===Convo2===
->Greeting12->
I never do anything I don't want to do. Do you?
    *[Of course!]
        Ugh. Why? I mean, I've heard that if humans don't do certain things their bodies waste away.
        That sounds awful! Could you imagine eating every day!?
        **[I like eating.]
            Well. That's good! Because I guess you will be doing it every day.
        **[Ugh, right?!]
            Right! No eating for us. Disgusting habit.
        **[I can't imagine!]
            I tried it once. Was deeply unpleasant. Food is always icky or crumbly or sticky.
        --Glad we talked this out, Tree Human.
    *[I have to work.]
        Right! Working! So you can get money!
        Spirits don't really have needs the way you do. 
        I hear being human is amazing because your bodies experience all kinds of stuff.
        Sounds exhausting to me. Giving your time to someone else just to live? no way.
    *[Me neither!]
        Yes! Finally, a human who understands!
        That's also good to hear because that means you WANT to be here helping the tree grow. You're a good person!
    - ~ currentStoryKnot = "About4"
->DONE


===Convo3===
->Greeting16->
While you were working I visited every spirit within a mile of here. It was exhausting.
I never want to miss out on anything, so I tend to say yes to everything.
    *[Me too!]
        I would say we should work on that, but honestly I don't want to.
        Like, doing things is so fun and not doing things is boring. Do it all!
    *[You should say no sometimes!]
        Yeah! I'm going to start saying no to things!
        ...but what if I say no and miss out on something awesome?
        ...that would be horrible.
    -Hah! I say, "NO!" to saying, "No!" Now I have the power of denying something but all the fun of doing things.
    Thank you!
->DONE



===Greeting1===
What's going on?
->->
->DONE

===Greeting2===
How are you tonight?
->->
->DONE

===Greeting3===
I heard humans like to geet each other by discussing the sky.
What's up?
->->
->DONE

===Greeting4===
Yes! It's the Tree Keeper!
->->
->DONE

===Greeting5===
Hi! Hey. Listen.
Between you and me, I don't much care for formal titles.
I mean, you're a great Tree Keeper, but...
Nobody calls me "They who alights the sky." Which would be horrific. So I prefer it that way.
->->
->DONE

===Greeting6===
Tree Human! How are you?!
->->
->DONE

===Greeting7===
What's going on tonight?
->->
->DONE

===Greeting8===
Well if it isn't my friend the Tree Keeper?
->->
->DONE

===Greeting9===
What's up in the sky this evening?
->->
->DONE

===Greeting10===
Heyo Tree Human! 
->->
->DONE

===Greeting11===
What's going on tonight, Tree Human?
->->
->DONE

===Greeting12===
Hi! Hey! Listen.
Just saying high, to be honest.
->->
->DONE

===Greeting13===
We are under attack by shadow demons!
Hahaha, just kidding. Demons can't come into the human world.
Anyway, good to see you!
->->
->DONE

===Greeting14===
It's Tree Human!
Hi, Tree Human!
->->
->DONE

===Greeting15===
Don't be fooled by my tough exterior, I'm actually twice as tough on the inside.
->->
->DONE

===Greeting16===
What's happening in the human world? Only good things? As if!
->->
->DONE