# HLP Project: by Hao Hao(01063260)

## How will you code contribute to the group:  1/4 page
How will (or might) your code contribute to the group deliverable? What have you done to ensure interfaces etc will be compatible? What are your interfaces (enough information for your module to be used by someone else not in your team. Assessment here is based on best efforts while allowing independent development, not whether the code is actually useful. Typical length 1/4 page.

* To ensure the interface is compatible during the group deliverable:
The program have Parse and Execution as the main high-level function which have the interface below:
    * `Parse:DataPath->LineData->Result<Parse<Instr>,string> option`
    * `Execution:DataPath->Parse<Instr>->DataPath`
    
* The overall interface is shown in the flow chart below:

![Diagram](https://github.com/ichh1915/ARM/blob/master/FlowChart.png)

* The following functions are compatible with other group members' module and can be easily adopted with little modification:
  
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

* Flexible Second Operand(`FlexOperand2`) is calculated as `uint32`, before being parsed to parse<Instr>;`SetC` is parsed as `bool option` representing the `C flag` update status when calculating `FlexOperand2`.
* For `Rs` during shift operations, only the least significant `byte` is used and can be in the range of `0-255`.
* For `#n` during shift operations, only the least siginificant `5-bits` are used and can be in the range of `0-31`, this feature is not implimented in VisUAL, the reasoning is to eliminate redundant number of shift(`#n`).
* The immediate literals are tested to be creatable by rotating a 8-bit number right within a 32-bit word.
* Updating Flags:
  * `Shift Operations`:The `C flag` is updated to the last bit shifted out, except when the shift length is 0. `N and C` are updated according to the result.
  
  * `Bitwise Operations` & `mov/mvn` & `tst/teq`:The `C flag` can be updated during the calculation of the `FlexOperand2`. `N and C` are updated according to the result.
  
  


* Features:
#### `TokenizeOperands.fs`- 
* Tokenize and parse the operand string:

####  `SFT.fs` -
* `CheckCond` : `DataPath`->`Condition`->`bool` which is compatible for all conditional operations.
* `ShiftExecute`: `DataPath->Parse<Instr>->DataPath`

#### `BIT.fs` -
* `BitwiseExecute`: `DataPath->Parse<Instr>->DataPath`

####  `TST.fs` -
* `TestExecute`: `DataPath->Parse<Instr>->DataPath`

## Test Plan:  1/2 page + table
A short description of your Test Plan. Typical length 1/2 page + tables. What you have tested will be clear from the feature specification which includes test status. How you have tested it must be itemised. Again a table is good (could be the same one as used for specification). Add any rationale for your test plan.
Specification showing what have been tested and the test status
How you have tested it ITEMISED
And any rational for you test plan

The Tests are desgined as unit tests with randomised initail states
* The specific Test method is as following:
  * Generate random initial R0-R14 register contents;
  * Generate random initial NZCV Flags;
    * Error when N and V are noth true.
  * Initialise assembly instructions covering all realised operations and operation modes;
  * Feed the above initial states as DataPath and LineData in to the F# `someExecution:DataPath->Parse<Instr>->DataPath`;
  * Input the initial states as Params and string to `RunVisualWithFlagsOut->Params->string->Flags->VisOutput`;
  * Test the output states for F# assembler and visUAL, namingly the updated register contents and the updated flags are **equal** .

The four types of operations are represented as symbols:

Operations | Symbol
------------ | -------------
`LSL,LSR,ASR,ROR,RRX`| SFT
`AND,ORR,BIC,EOR` | BIT
`MOV,NVN` | MOV
`TST,TEQ` | TST

Test assembly line | Status
------------ | -------------
`SFT{S} Rd, Rn, #n` with `#n>=32` | Expected to `Fail`,due to only `literial input &&& 0x1F` are used for shift operations,visUAL allow number of shift to be `>32`
`SFT{S} Rd, Rn, #n` with `#n<32` | `Success`
`SFT{S} Rd, Rn, Rs` | `Success`
`RRX{S} Rd, Rm` | `Success`
`SFT{S} Rd, Rn, Rs in the cases Rd=Rn` | `Success`
`BIT{S} Rd, Rn, #n` | `Success`
`BIT{S} Rd, Rn, Rs` | `Success`
`BIT{S} Rd, Rn, Rs, SHIFT, #n` | `Success`
`BIT{S} Rd, Rn, Rs, SHIFT, Rm` | `Success`
`MOV{S} Rd, #n` | `Success`
`MOV{S} Rd, Rs` | `Success`
`MOV{S} Rd, Rs, SHIFT, #n` | `Success`
`MOV{S} Rd, Rs, SHIFT, Rm` | `Success`
`TST{S} Rn, #n` | `Success`
`TST{S} Rn, Rs` | `Success`
`TST{S} Rn, Rs, SHIFT, #n` | `Success`
`TST{S} Rn, Rs, SHIFT, Rm` | `Success`
`MVNS{Cond} "R5,R10,ROR R6`| Test all conditions. `Success` for sufficient number of test runs, each run with random initial flags
`MVNSNV R5,R10,ROR R6` | Expected to have `error`, NV condition is omitted by visUAL
`ORRS R1,R1,R3,RRX #1` | Expected to have `error`, the #1 for RRX is omitted by visUAL








