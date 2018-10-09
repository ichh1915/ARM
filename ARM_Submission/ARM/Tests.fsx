/// ARM execution conditions
    type Condition =

        | Ceq //Z set    
        | Cne //Z clear  
        | Cmi //N set
        | Cpl //N clear
        | Cvs //V set
        | Cvc //V clear
        | Chs //C set
        | Clo //C clear
        | Chi //C set and Z clear
        | Cls //C clear or Z set
        | Cge //N and V the same
        | Cgt //Z clear, N and V the same
        | Cle //Z set , N and V differ
        | Clt //N and V differ
        | Cnv // the "never executed" condition NV - not often used!
        | Cal // the "always executed condition "AL". Used by default on no condition

    /// classes of instructions (example, add/change this is needed)
    type InstrClass = | DP | MEM

    /// specification of set of instructions  class+root+suffixes
    ///                                       DP...  ADD...  S...EQ...
    type OpSpec = {
        InstrC: InstrClass
        Roots: string list
        Suffixes: string list
    }


    type SymbolTable = Map<string,uint32>

    /// result return from instruction-specific module parsing
    /// an instruction class. If symbol definitions are found in a 
    /// symbol table then a complete parse will be output
    /// otherwise some fields will be None
    type Parse<'INS> = {
            /// value representing instruction. NB type varies with instruction class
            PInstr: 'INS 
            /// name and value of label defined on this line, if one is.
            PLabel: (string * uint32) option 
            /// number of bytes in memory taken up by this instruction
            PSize: uint32 
            /// execution condition for instruction
            PCond: Condition
        }



    /// Strings with corresponding execution condition
    /// Note some conditions have multiple strings
    /// Note "" is a valid condition string (always execute condition)
    let condMap = [ "EQ",Ceq ; "NE",Cne ; "MI",Cmi ; "PL",Cpl ; "HI", Chi ; 
                    "HS",Chs ; "LO",Clo ; "LS",Cls ; "GE",Cge ; "GT", Cgt ; 
                    "LE", Cle ; "LT", Clt ; "VS",Cvs ;  "VC",Cvc ;
                    "NV",Cnv ; "AL",Cal ; "",Cal] |> Map.ofList

    /// list of strings representing all execution conditions
    /// includes ""
    let condStrings = 
        condMap
        |> Map.toList
        |> List.map fst
        |> List.distinct    

    /// generate all possible opcode strings for given specification
    /// each string is paired with info about instruction
    /// and the three parts of the opcode
    ///                                   opcode    class        root    suffix   instr cond
    let opCodeExpand (spec: OpSpec) : Map<string, InstrClass * (string * string * Condition)> =
        spec.Roots
        |> List.collect (fun r -> 
            spec.Suffixes
            |> List.collect (fun s -> 
                condStrings
                |> List.map (fun c -> r+s+c, (spec.InstrC,(r,s, condMap.[c])))))
                |> Map.ofList

    let spc:OpSpec = {
         InstrC=DP
         Roots=["ADD";"SUB"]
         Suffixes=["";"S"] }


    let map = opCodeExpand spc



    let instf = map.["ADDSEQ"]

    let cond = instf

    printfn"The result is %A" cond


    System.Console.ReadLine()


    ////////////////////////////////////////////////////////////////////////////////
    /// execute code - this does not include the condition
    /// but that can be easily implemented
    /// this function executes the instruction ins
    /// and returns DataPath d changed as expected by the
    /// instruction
    /// runErrorMap is is a function which adapts the module runtime error type
    /// to the toplevel type. Similar to pResultInstrMap in the provided code
    /// if all runtime errors have the same type (e.g. string) it is not needed.

    //let executeAnyInstr (ins: Instr) (d: DataPath): Result<DataPath, ErrRun> =
    //    let execute =
    //        match Instr with
    //        | IXX ins -> XX.execute |> runErrorMap
    //        | IYY ins -> YY.execute |> runErrorMap
    //    execute d


    ///// must include all changeable machine state, 
    ///// including writeable memory, registers, flags
    //type DataPath = ...


    ///// allows different modules to return different instruction types
    //type Instr =
    //    | IMEM of Memory.Instr
    //    | IDP of DP.Instr

    ///// allows different modules to return different parse error info
    ///// by default all return string so this is not needed
    //type ErrInstr =
    //    | ERRIMEM of Memory.ErrInstr
    //    | ERRIDP of DP.ErrInstr
    //    | ERRTOPLEVEL of string

    ///// allows different modules to return different parse error info
    ///// by default all return string so this is not needed
    ///// NB - these are not yet implemented in sample code
    //type ErrRun =
        //| RUNERRMEM of Memory.ErrRun
        //| RUNERRDP of DP.ErrRun
        //| RUNERRTOPLEVEL of string
