# FunBooks

This is an exercise based on [David Thomas' Kata 16](http://codekata.com/kata/kata16-business-rules/)

## The initial idea

Clearly to eliminate the need to have to change the code constantly a way to provide the ability to modify from an external source is needed.
So a DSL is needed to create the solution. You can either create your own DSL or you can use some existing dialect to drive the creation of the DSL.
On this specific case I have started with the second option, and I am using YAML to codify the rules. Especially as the Purchase Order does look already like YAML. 
For this to work, a comprehensive way to be able to do the actions is required through an API. IPOModifer will provide the methods that will need to be implemented.

## Issues so far
The main issue found has been around the technology used. Because I have started this on my holidays, I only had my Linux laptop available. 
Therefore, it was time to learn how to use .Net Core. Which has been interesting to say the least. Even more, as there are libraries only present on a prerelease form (Moq)
or not present at all (any YAML library)


## ToDo
- Finish setting up the rules
- Finish the implementation of IPOModifier
- Try to store variables from the rules to be used as parameters on the actions.
- Refactor Apply method (can it be extracted to a text file?)