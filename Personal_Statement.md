# HLP Project  
## Hao Hao(hh1915)

## Work and contribution during the group phase 

Implemented the parsing and execution of DCD, DCB, FILL, EQU and END instructions which was not done during the individual phase. Due to these instruction are excuted at the top level, it is done with help from team member Zheng yang Lee's work which includes commonTop.   

Added following useful functions
1. `Op2SetCFlag:Datapath->FlexOp2->bool option` | Parse the `C flag` update status during the calculation of the flexible Op2
2. `checkCond:DataPath->Condition->bool` | Check whether the flags status match the condition during the conditional operation
    
    









