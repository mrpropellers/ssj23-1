VAR currentStoryKnot = "Greet"

This is much nicer than where I was.
I guess I'll stay here at night from now on.
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

Some days I don't feel much like doing anything.
But on those days I don't even enjoy doing nothing. 
So each day I always try and do something. Even if it's small.
~ currentStoryKnot = "Tip3"
->DONE


===About2===
->Greeting9->

Some spirits would say that I don't do well outside my comfort zone as other spirits do.
But now I know that I just found what I actually like when I was quite new.
It's frustrating when people don't believe me, but I know what I like and it makes me happy to stick with those things.

~ currentStoryKnot = "About3"
->DONE


===About3===
->Greeting10->

Sometimes when I feel overwhelmed by everything I just decide to do something for a two minutes.
By the end of two minutes I'm usually okay to keep going.
~ currentStoryKnot = "Encourage4"
->DONE


===About4===
->Greeting13->

Once I realized that the goals I set for myself are the most important, life became much less stressful.
Other people's expectations can be exhausting.
~ currentStoryKnot = "Encourage5"
->DONE


===About5===
->Greeting15->

I feel like I've learned so much by being here. 
I came here for a break from my life, and I got that.
But now I'm ready to dive back in, rested and recharged. Thank you!
~ currentStoryKnot = "Convo3"
->DONE


===Encourage1===
->Greeting2->

So much work ahead can be daunting... I'm happy you're not deterred!
~ currentStoryKnot = "Tip2"
->DONE


===Encourage2===
->Greeting4->

Watching you work always makes me feel better. 
It inspires me to do things as well!
~ currentStoryKnot = "About1"
->DONE


===Encourage3===
->Greeting8->

I admire how you always come back to help the tree grow. 
It inspired me to visit other spirits.
Maybe by being there I help them grow?
~ currentStoryKnot = "About2"
->DONE


===Encourage4===
->Greeting11->

Thank you for always talking to me - you're a good listener.
I bet that helps you go wherever you want to go as a human!
~ currentStoryKnot = "Convo2"
->DONE


===Encourage5===
->Greeting14->

I feel like it's always much, much harder to do things than it is to do nothing. But you're here, always doing things.
I guess I just wanted to say that I'm proud of you.
~ currentStoryKnot = "About5"
->DONE


===Tip1===
->Greeting1->

We spirits rest during the day and hide in the tree. We wouldn't want to distract you from focusing!
~ currentStoryKnot = "Encourage1"
->DONE


===Tip2===
->Greeting3->

If you don't prune the tree, it will grow too many small branches.
Each branch takes energy from the tree, so it makes it hard to grow.
In other words, just like people, the tree can overextend itself.
~ currentStoryKnot = "Encourage2"
->DONE


===Tip3===
->Greeting6->

Sometimes I feel sad for the branches that are pruned.
But then I remember they'll become soil, and then maybe a branch again!
Sometimes the big picture makes things better.
Other times it just feels overwhelming.
~ currentStoryKnot = "Convo1"
->DONE


===Convo1===
->Greeting7->
VAR silence = 0
I don't have a lot of social energy right now. 
Want to just sit together for a bit? ->waiting
=waiting
    +[Stay and wait.] {...|...|...} 
        ~ silence += 1
        {silence >2: 
        ->silencebroken
        }
        ->waiting
    ->silencebroken
    *[Get up to go.] ->silenceend
->DONE
=silencebroken
Thank you for staying with me! 
I feel you can tell a true friend when they're happy to just be near you.
Some people get antsy when it's when quiet, but companiable silence is comforting to me. 
I hope you have a great day tomorrow!
->DONE

=silenceend
Thanks for staying awhile! Have a nice day!
What do you do to get moving?
~ currentStoryKnot = "Encourage3"
->DONE


===Convo2===
->Greeting12->
Can I tell you something?
Today I saw a person making fun of one of the other spirits for liking books!
Books!? I know they're a human invention but...
Well, spirits commune with each other so the activity of reading is seen as... rather eccentric.
...I just get so angry when people make fun of the things others enjoy.
    *[People should let people enjoy things!]
    *[Reading doesn't hurt anyone!]
        It doesn't! There's no reason to 
    *[Is it silly for a spirit to read?]
        I'm assuming you're being sarcastic. I think it's silly for anyone to do most things, to be honest.
    - ~ currentStoryKnot = "About4"
->DONE


===Convo3===
->Greeting16->
Many of the spirits I've met all enjoy being together. Sometimes they gather in flocks! 
I'm happier with a small handful of friends. Which do you prefer?
    *[Large groups!]
        Every now and then I find that thrilling! Parties, crowded places... I've even visited a stadium!
        But I do get tired quickly. I would get grumpy and I didn't realize I was overstimulated. 
    *[Small groups.]
        It took me a long itme to realize that I don't always have energy to be social.
        If I'm conservative with being around so many people, I enjoy it more when I do.
    - Just goes to show it's important to take care of yourself by knowing yourself! 
    Then you can be the best version of you most often.

->DONE



===Greeting1===
Hello...
->->
->DONE

===Greeting2===
Oh, hey.
->->
->DONE

===Greeting3===
Hello there...
->->
->DONE

===Greeting4===
Hi!
->->
->DONE

===Greeting5===
Oh... Hi!
->->
->DONE

===Greeting6===
Hello, again.
->->
->DONE

===Greeting7===
Hi, there!
->->
->DONE

===Greeting8===
Hello... Tree Keeper? 
It feels so formal to say that but it's nice to remember the old ways sometimes.
->->
->DONE

===Greeting9===
Hello, there! How are you?
->->
->DONE

===Greeting10===
Hi! It's nice to see you.
->->
->DONE

===Greeting11===
Oh! Hi!
->->
->DONE

===Greeting12===
Hello! Welcome back!
->->
->DONE

===Greeting13===
Oh, hey! How are you?
->->
->DONE

===Greeting14===
Hi! Do you think it's a nice evening? I do!
->->
->DONE

===Greeting15===
Hello again!
->->
->DONE

===Greeting16===
Oh! Hello, friend!
->->
->DONE