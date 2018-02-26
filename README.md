# HLP ARM Assembly Project: Hao Hao(01063260)

## How will you code contribute to the group:  1/4 page
How will (or might) your code contribute to the group deliverable? What have you done to ensure interfaces etc will be compatible? What are your interfaces (enough information for your module to be used by someone else not in your team. Assessment here is based on best efforts while allowing independent development, not whether the code is actually useful. Typical length 1/4 page.

* To ensure the interface is compatible during the group deliverable:
The program have the following high level interface:
    * `Parse:DataPath->LineData->Result<Parse<Instr>,string> option`
    * `Execution:DataPath->Parse<Instr>->DataPath`
    
* The following functions are compatible with other members' module and can be easily used little modification:
  
Function | description
------------ | -------------
`tokenize:string->TokenList` | Tokenize a string of all operands
`flexOp2:Datapath->FlexOp2->uint32` | Calculate the flexible second operand as `uint32`
`Op2SetCFlag:Datapath->FlexOp2->bool option` |  Parse the `C flag` update status during the calculation of the flexible Op2
`checkCond:DataPath->Condition->bool` | Check whether the flags status match the condition during the conditional operation
  
    





## What is the Specification of your code:  1/2 pages
What is the specification of your code? Detail differences from VisUAL (if doing standard project), and reasons for them. Detail any areas where spec was initially unclear and has been clarified. Typical length 1/2 page + Tables.
Your markdown file can refer to comments in code, or the code itself, for details of normal functionality.
Your markdown file should contain a precise description of how much functionality has been implemented, and how much tested (tables of features are good for this).
A precise specification document would be very long: your document should only detail issues not obvious from the initial spec that needed to be resolved. An example of this for the default project would be where upper/lower case is significant, and where not.

* Functionalities:

Operations | Syntax
------------ | -------------
`LSL,LSR,ASR,ROR,RRX` | `op{S}{cond} Rd, Rn, Rs` or  `op{S}{cond} Rd, Rm, #n` or `RRX{S}{cond} Rd, Rm`
`AND,ORR,BIC,EOR` | `op{S}{cond} Rd, Rn, FlexOperand2`
`MOV,NVN` | `op{S}{cond} Rd, FlexOperand2`
`TST,TEQ` |`op{cond} Rn, FlexOperand2`

* For `Rs`, only the least significant `byte` is used and can be in the range of `0-255`.
* For `#n`, only the least siginificant `5-bits` are used and can be in the range of `0-31`, this feature is not implimented in VisUAL, the result is the elimination of redundent number of shift(`#n`)
* The immediate literals are tested to be creatable by rotating a 8-bit number right within a 32-bit word.
* The `C flag` can be updated during the calculation of the `FlexOperand2`.


* Features:
#### `TokenizeOperands.fs` -Tokenize and parse the operand string:
   * For `bitwise logic` operations and `mov/mvn` with the syntax:`op{S}{cond} Rd, Rn, Operand2`, the flexible second operand(`Operand2`) is calculated and parsed as `uint32`. Also, the `Flag C` update status resulted from shift in operand2 is parsed.
   * For the shift operations with the following syntax:
      `op{S}{cond} Rd, Rm, Rs`,
      `op{S}{cond} Rd, Rm, #n`  and
      `RRX{S}{cond} Rd, Rm`.
     For `Rs`, only the least significant `byte` is used and can be in the range of 0-255.
     For `#n`, only the least siginificant `5-bits` are used and can be in the range of 0-31, this feature is not implimented in VisUAL, the dicision is made to elimilate redundent number of shift(`#n`)
   * The immediate literals are tested to be creatable by rotating a 8-bit number right within a 32-bit word.

####  `SFT.fs` -
* `CheckCond` : `DataPath`->`Condition`->`bool` which is compatible for all conditional operations.
* `ShiftExecute`: `DataPath->Parse<Instr>->DataPath`

#### `BIT.fs` -
* `BitwiseExecute`: `DataPath->Parse<Instr>->DataPath`

####  `TST.fs` -
* `TestExecute`: `DataPath->Parse<Instr>->DataPath`

## A short discription of the Test Plan:  1/2 page + table
A short description of your Test Plan. Typical length 1/2 page + tables. What you have tested will be clear from the feature specification which includes test status. How you have tested it must be itemised. Again a table is good (could be the same one as used for specification). Add any rationale for your test plan.
Specification showing what have been tested and the test status
How you have tested it ITEMISED
And any rational for you test plan



F# Program files under VisualTest:

* `VCommon.fs` Base types used throughout
* `VData.fs` Auto-create assembler fragments for interfacing
* `VLog.fs` Primitive serialisation interface for cache. Written without dependencies.
* `Visual.fs` Drive the Visual command-line program, implementing parallel opration and cacheing
* `VTest.fs` Tests are the framework itself, and some sample assember tests
* `VProgram.fs` Top-level code




graph TD;
    A-->B;
    A-->C;
    B-->D;
    C-->D;

