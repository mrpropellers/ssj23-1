VAR currentStoryKnot = "Greet"

How wonderful! I knew I could sense someone doing fine work!
I'll be sure to visit each evening. 
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

I've known a few humans in my time. I've liked all of them for different reasons.
~ currentStoryKnot = "Tip3"
->DONE


===About2===
->Greeting9->

Humans are so busy, busy, busy.
I love watching you all - there's so much to learn.
~ currentStoryKnot = "About3"
->DONE


===About3===
->Greeting10->

I miss my home. I feel lighter and brighter when I'm away for a little bit, but in the end I belong among my people.
~ currentStoryKnot = "Encourage4"
->DONE


===About4===
->Greeting13->

It's important to accomplish what you want to accomplish.
It's also important to fill your life with things that bring you joy.
They don't have to be the same thing!
~ currentStoryKnot = "Encourage5"
->DONE


===About5===
->Greeting15->

One of my favorite things to do is find a place among the branches that everyone tends to pass. Then I can say hello to everyone while resting!
Plus, I get to catch up on all the gossip and goings on.
~ currentStoryKnot = "Convo3"
->DONE


===Encourage1===
->Greeting2->

If you're taking time to talk to me, then I know you're taking care of yourself with a break!
Putting your mind on something else, even briefly, is like coming up for air out of the water.
It may not feel like you need it early in on, but you'll run out of air faster by the end if you don't.
~ currentStoryKnot = "Tip2"
->DONE


===Encourage2===
->Greeting4->

If everyone just took a deep breath and set about their tasks the way you do, the world would be a better place.
~ currentStoryKnot = "About1"
->DONE


===Encourage3===
->Greeting8->

The tree is remarkable. No matter how much you prune, it will always grow back stronger.
Just like you. You will always grow back.
~ currentStoryKnot = "About2"
->DONE


===Encourage4===
->Greeting11->

When I was a new spirit, several friends and I spent a day with a tree like this one.
The tree was filled with apples! We shook the tree and all the apples fell out. In the morning, the farmers were amazed and took the apples to bake pies.
This reminds me so much of that day and those friends. Thank you!
~ currentStoryKnot = "Convo2"
->DONE


===Encourage5===
->Greeting14->

You're a remarkable person, to have grown this little home for all these spirits. Thank you!
~ currentStoryKnot = "About5"
->DONE


===Tip1===
->Greeting1->

Can I let you in on a little pruning secret?
Everyone has feelings on what makes a tree beautiful. 
But in the end all that matters is that the tree is beautiful to you.
~ currentStoryKnot = "Encourage1"
->DONE


===Tip2===
->Greeting3->

Pruning is like setting boundaries. 
You have to decide where you want to grow, and what you want to leave behind.
~ currentStoryKnot = "Encourage2"
->DONE


===Tip3===
->Greeting6->

Pruning isn't about choosing what branches you dislike. It's about choosing the branches you know you love, and letting go of the rest.
~ currentStoryKnot = "Convo1"
->DONE


===Convo1===
->Greeting7->

In the spirit world, we don't have parents. We arrive in the spirit realm as full persons, ready to help! However, we often take on a mentor to help guide us through our early years. 
Do you have a mentor? Someone who helps you make decisions about your life path? 
    *[I do!]
        Well, that is excellent. They've done a great, I feel.
    *[I do not.]
        It can feel awkward to ask, but if you find someone you respect whose life looks like what you want your to look like, you should consider asking. 
        A mentor can be a powerful ally against demons and all sorts of spirit world enemies.
        Although, I suppose in the human world they could help you grapple with hard decisions more than slay monsters.
    - ~ currentStoryKnot = "Encourage3"
->DONE


===Convo2===
->Greeting12->
VAR workRating = 0
How is your work going today?
    *Excellent[!]<>
        ? That's good to hear. 
        ~ workRating = 1 
    *Just fine[.]<>
        ? Oh my, that doesn't sound promising.
        ~ workRating = 2
    *Not well[.]<>
        ? I'm sorry to hear that. But look at you, pushing through.
        ~workRating = 3
    -
What is it that you're doing?
    *[Work for my job.] -> job
    *[Stuff for my hobbies.] ->hobbies
    *[Chores!] ->chores

=job
    {workRating == 1: 
        Well, that's good to hear. Sometimes work can be such a burden! It's only easier when you've got a good feeling for it!
    }
      
    {workRating == 2: 
        There's nothing wrong with an average day. I would never say, "It could be worse!" What a useless sentiment!
    } 
    {workRating == 3:
       Oh, my! A tough day doing work for an employer instead if yourself can be extra tiring.
      I know you can tough it out. 
    }
->convo2Finish

=hobbies
        {workRating == 1:
            Oh, glorious! To have time to spend on what you love and for it to go well!
        }
        {workRating == 2: 
            Fine? I hope it's not too exhausting, if it's a hobby. Some things bring me joy no matter how poorly they go.
            Sometimes the things that bring joy are extra disappointing when they don't go well. 
            Don't lose heart - it will come back around for you!
        } 
        {workRating == 3:
            Oh, how disappointing! 
            When we love doing something and it goes poorly, it can be extra frustrating.
            Hang in there! The joy will return!
        }
->convo2Finish

=chores
        {workRating == 1: 
        Oh, excellent! It's nice to see you taking care of yourself by putting effort into your own situation.
            Keep it up! You're building your own happiness!
        }
        {workRating == 2:
        I hope it's not too tedious. Don't forget that chores are how you build your own happiness at home.
        You deserve the results of your work! Keep going!
        } 
        {workRating == 3:
         Oh, no! Chores going poorly can be so discouraging. Remember that, really, you're doing this for you! 
         Chores are a form of responsible self care, my friend. Keep going! You're building your own happiness!
        }
->convo2Finish

=convo2Finish
~ currentStoryKnot = "About4"
->DONE


===Convo3===
->Greeting16->
I've grown quite fond of this tree... these spirits...
And you! Of course! 
Do you think you'll look back fondly on all of this?
    *[Of course!]
        That is quite kind of you to say, and I do believe you.
        The say that the biggest indicator of friendship is proximity!
        You grow closest to the things you spend the most time with.
        Thank you for spending time here, my friend.
    *[It would be rude to say otherwise.]
        Hah! So it would, you rogue! Don't toy with an old spirit's heart like that!
        The say that the biggest indicator of friendship is proximity!
        You grow closest to the things you spend the most time with.
        Thank you for spending time here, my friend.
        -
->DONE



===Greeting1===
Well, a star-filled evening to you, young Tree Keeper.
->->
->DONE

===Greeting2===
You're always a good sight for old eyes, Tree Keeper.
->->
->DONE

===Greeting3===
What a delight that you're here!
->->
->DONE

===Greeting4===
A star-filled night to you, my friend.
->->
->DONE

===Greeting5===
Excuse me, Tree Keeper? Have a second for an old spirit?
->->
->DONE

===Greeting6===
By the stars, it's nice that you're here.
->->
->DONE

===Greeting7===
Be a dear and chat with me, Tree Keeper?
->->
->DONE

===Greeting8===
Hello, hello, young Tree Keeper.
->->
->DONE

===Greeting9===
I'll say again! It's a delight that you're here!
->->
->DONE

===Greeting10===
Is that you, Tree Keeper? Of course it is!
->->
->DONE

===Greeting11===
Good evening, my dear friend.
->->
->DONE

===Greeting12===
And a fine evening to you as well, my dear.
->->
->DONE

===Greeting13===
What a delightful surprise! Hello!
->->
->DONE

===Greeting14===
Ah, yes. It's always good for this old spirit to see you.
->->
->DONE

===Greeting15===
Hello, hello, young friend.
->->
->DONE

===Greeting16===
A star-filled night to you, Tree Keeper.
->->
->DONE