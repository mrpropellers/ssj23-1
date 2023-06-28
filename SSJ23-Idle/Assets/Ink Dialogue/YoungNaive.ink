VAR currentStoryKnot = "Greet"
VAR writingThingsDown = "False"
Is this your tree?? I didn't know humans cared for us spirits. 
I hope you don't mind, but I'll be here every sunset for now.
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

Spirits like me are small and the world is big and exciting and scary.
I'm glad you're in it!
~ currentStoryKnot = "Tip3"
->DONE


===About2===
->Greeting9->

Are all humans always doing things? 
You're the first human I've ever met.
I did a good job of picking someone, didn't I?!
~ currentStoryKnot = "About3"
->DONE


===About3===
->Greeting10->

Look at how the light comes in through the branches.
Every night it looks different and I always find something new to love!
~ currentStoryKnot = "Encourage4"
->DONE


===About4===
->Greeting13->

I can't tell if I like the low branches or the high branches more!
High branches have a better view but low branches feel cozier.
Maybe I can like them both!
~ currentStoryKnot = "Encourage5"
->DONE


===About5===
->Greeting15->

Maybe after this I'll do something completely different and live in a big city!
I was told that I need to try new things to figure out what I really like, so I'm trying to do that.
~ currentStoryKnot = "Convo3"
->DONE


===Encourage1===
->Greeting2->

I don't understand what you're doing, but I can tell it's impressive.
~ currentStoryKnot = "Tip2"
->DONE


===Encourage2===
->Greeting4->

Oh golly! This is incredible! You're doing an incredible job.
...do humans say, "golly?"
~ currentStoryKnot = "About1"
->DONE


===Encourage3===
->Greeting8->

Honestly...
I think the biggest challenge sometimes is just showing up.
You're here! You showed up for us! And for yourself! You already beat the biggest challenge!
{writingThingsDown == "True":
    Also I tried some of the planning stuff we discussed and it really helped! Thank you!"
    }
~ currentStoryKnot = "About2"
->DONE

===Encourage4===
->Greeting11->

I wanted to get all the spirits to do a chant together because you deserve it! 
But we are bad at coordinating. 
So I'm chanting in my heart for you! You can do it!
~ currentStoryKnot = "Convo2"
->DONE


===Encourage5===
->Greeting14->

Tree keeper!
Tree. Keeper. Tree. Keeper. Tree. Keeper.
TREE KEEPER TREE KEEPER TREE KEEPER!
Good job! 
That was my chant.
~ currentStoryKnot = "About5"
->DONE


===Tip1===
->Greeting1->

It doesn't hurt the tree to prune it. 
Don't worry, I asked them.
~ currentStoryKnot = "Encourage1"
->DONE


===Tip2===
->Greeting3->

The first tree I ever lived in the Keeper rarely ever trimmed it, but then would trim it all at once!
It doesn't hurt the tree either way, whether you trim all at once or do a little every night.
~ currentStoryKnot = "Encourage2"
->DONE


===Tip3===
->Greeting6->

Some folks would say that I don't prune enough because I'm nervous about making a mistake. 
But I actually like my tree to be more like a jungle! 
I guess mistakes can also be fun.
~ currentStoryKnot = "Convo1"
->DONE


===Convo1===
->Greeting7->

When I have things I need to get done, I tend to dive in too fast and then I get overwhelmed. 
    *[What does a spirit need to do?]
        Oh! Well we commune with other spirits. We admire the sky and the trees and the world. We meditate on the meaning of things. We act as guardians for fundamental aspects of the universe. We have a lot of responsibilities.
            Nothing as serious as your responsibilities, but everyone is fighting their own battle!
    *[Make a plan to pace yourself!]
        -> Solutions
    *[I do the same thing.]
        We have something in common!! I like that.
        At least we know we're doing it. We just need to find a better way to do things.
    ~ currentStoryKnot = "Encourage3"
    -Thanks for talking to me about it!
    ->DONE
=Solutions
    A plan sounds smart! What's a plan?
        *[You write things down.]
        *[You decide what to do before you do it.]
        -Ohhhh! That would help. Then I wouldn't have to worry so much about remembering what I need to do before I do it.
        I'll give that at try!
        ~currentStoryKnot = "Encourage3"
        ~writingThingsDown = "True"
->DONE


===Convo2===
->Greeting12->
Do you like to read?
    *[Yes, every day!]
        Every day?! That's INCREDIBLE. I didn't know you could do that! What do you like to read?
            ** [True stories.]
                How many true stories could there be?! If you read them every day, then there must be so many!
            ** [Made up stories.]
                Things that are made up?! How many stories can humans possibly mkae up?!
                I guess if you read every day, there must be so many!
            ** [Everything.]
                That makes sense! If you like to read every day, then you would need a lot of options!
    *Sometimes[.]<>
        ? Oh! Is something keeping you from reading?
        **[I only like certain things.]
            It's good to know what you like! I still read things I hate all the time because I'm trying to figure out what I do and don't like.
            Yesterday I read all of the stop signs in the city. Ugh. So boring. I have to be careful to never do that again.
        **[I'm often too busy to read.]
            That's not a bad thing! It means the times when you do get to read are extra special! If you had to read all the time...
        **[I normally read for work.]
        -- Isn't it so strange how something we love maybe loses its appeal when we're required to do it?
            That's why I keep at least one or two things in my life that I hate doing.
            If my life was only things I loved, then maybe I'd start hating those things on accident!
    *[It's not for me.]
        That's okay! Not everything has to be everybody. I love to read, but I bet there's other things we have in common. Do you like kites? Or puppies? Or food?
            **[I love kites!]
                Oh, wow! That is brave of you. Kites remind of demons from the spirit realm, so I find them spooky. How we're different is so interesting!
            **[I love puppies!]
                Same here! Puppies are the best. Kittens are pretty great, too. Maybe it's kind of a lazy attempt at common ground because everyone likes puppies.
                But it's still nice to find something in common, even if it's obvious.
            **[I love food!]
                Me too! I can't eat food, but I love everything else about it! Lots of people like food but they don't love it!
            - - Everyone is so different yet so similar. That's what makes life interesting.
    - Even a conversation about reading can be almost as fun as reading! Thanks for talking to me!
~ currentStoryKnot = "About4"
->DONE


===Convo3===
->Greeting16->
Have you figured out what you want to do with your life?
    *[That's a very serious question.]
        Is it? Spirits live for exactly as long as we want to, so I guess we have as much life as we want to figure out what we want to do with it.
        Sorry if that was too personal! 
            ** [It's okay!]
                Whew! I'm glad to hear it.
            ** [Let's talk about something else.]
                Okay! Uhm. Oh! I'll think about it and get back to you!
            -- Until next time!
    *[I think I have.]
        Wow! That must feel amazing! Good for you! Spirits live as long as we like, so some of us never really decide because we never have to. I envy you.
        I'll keep watching you work and see what I can learn!
    *[I'm still working on it.]
        Me too! How are you deciding what to do?
            **[Trying new things.]
                Experimenting is smart! The more things we try the more we'll know what we do and don't like. I'll try more things!
            **[Talking to others about it.]
                Same! Talking to other spirits helps me sort out at my thoughts. Sometimes it's easy to think you have to figure it out alone.
            **[Learning from others.]
                Oh! Like in school? Or in training? Spirits don't really have a school but sometimes we go to human schools.
                Well. We don't attend as students. We just watch others!
            -- Good luck with doing things with your life! I have a good feeling you'll do it right. Whatever that means for you.
    - ->DONE
->DONE

===Greeting1===
Hiya! Thanks again for letting me stay.
->->
->DONE

===Greeting2===
Hello again, friend!!
I hope it's cool that I call you my friend!
->->
->DONE

===Greeting3===
Hiya! I hope you got everything done that you wanted to get done.
->->
->DONE

===Greeting4===
Hey! If I was bigger, I'd see if we could high five!
->->
->DONE

===Greeting5===
Hello, hello, hello, my friend!
->->
->DONE

===Greeting6===
Hiya! Is it evening already? Wow, where did the time go?
->->
->DONE

===Greeting7===
Hiya! In case I haven't said it recently, thank you again for letting me stay here.
->->
->DONE

===Greeting8===
Oh no! I was hoping to rest more! Oh well. I was up all day reading.
->->
->DONE

===Greeting9===
Hello, hello, hello! It's good to see you.
->->
->DONE

===Greeting10===
Heya friend! Happy to see you.
->->
->DONE

===Greeting11===
Oh hi! Is it really already evening? I finished a great book last night. 
->->
->DONE

===Greeting12===
Hello again, friend!
It IS cool that we're friends.
->->
->DONE

===Greeting13===
Hiiii! Can you believe I've been here almost two weeks?!
->->
->DONE

===Greeting14===
Heya Tree Keeper! Happy night time.
->->
->DONE

===Greeting15===
Hi! Hello! Hi!
->->
->DONE

===Greeting16===
Hello, hello, hello and I'm happy to see you!
->->
->DONE