# GFORTH_6502
6502 Complier on Gforth
## Summary Explaining the Assembler Code

This article provides an overview and explanation of the assembler code written in Forth and refactored for Gforth. The code is designed to be a 6502 assembler, and it includes various definitions and op-codes specific to the 6502 microprocessor.

### Register Assignment and Nucleus Locations

The code begins with the assignment of specific registers for the implementation, such as XSAVE, W, UP, and IP. These registers are constants that hold specific values used in the assembly process. Additionally, the code defines nucleus locations using the `(DO)`, `LIT`, and `EXECUTE` words.

### Assembler Continuation

The next section of the code deals with the continuation of the assembler. It includes the definition of variables like INDEX and MODE, which are used in the assembly process. The code also defines several words like `.A`, `#`, `MEM`, `,X`, `,Y`, `X)`, and `)Y`, which set the MODE variable to specific values for different addressing modes. Furthermore, there are words like `BOT`, `SEC`, and `RP)` that address specific locations in the stack and return stack.

### UPMODE and CPU Definitions

The UPMODE section defines the UPMODE word, which is used to check and manipulate the MODE variable based on certain conditions. The CPU section defines op-codes for the 6502 CPU, including instructions like BRK, CLC, CLD, CLI, etc. Each op-code is represented as a word that compiles the corresponding byte value into the assembly code.

### M/CPU and Multi-Mode Op-Codes

The M/CPU section introduces the M/CPU word, which is used to handle multi-mode op-codes. It checks the current MODE value and modifies it accordingly. It also includes a list of op-codes with corresponding byte values for instructions like ADC, AND, CMP, LDA, etc.

### Assembler Conditionals

The Assembler Conditionals section defines conditional words like BEGIN, UNTIL, IF, THEN, and ELSE. These words control the flow of the assembly code based on specific conditions. For example, IF and ELSE allow branching in the code based on the value of the top of the stack.

### End Code and Code Definitions

The End Code section includes the definition of the `end-code` word, which marks the end of a code definition. It also sets the context and smudge status for proper code compilation. The Code Definitions section provides the `code` word, which creates a new word at the assembly code level. It allows for the compilation of assembly code and manipulation of the assembler.

### Locking the Assembler into the System

The final section locks the assembler into the system by updating various memory locations and variables. It ensures that the assembler becomes part of the system and can be used for assembly code compilation.

---

This article gives a high-level overview of the assembler code, providing insight into the different sections and their respective purposes. The code will be further customized or modified based testing. 
