MOV R0, #0x7FFFFFFF
ADDS R0, R0, R0
MOVS R0, #1
MOV R0, #0x7f
ADD R0, R0, #0xa400
ADD R0, R0, #0x40000
ADD R0, R0, #0xa000000
MOV R1, #0xdf
ADD R1, R1, #0xc500
ADD R1, R1, #0xdf0000
ADD R1, R1, #0x6000000
MOV R2, #0x4d
ADD R2, R2, #0xbf00
ADD R2, R2, #0x1a0000
ADD R2, R2, #0x5000000
MOV R3, #0x9b
ADD R3, R3, #0x3e00
ADD R3, R3, #0xcc0000
ADD R3, R3, #0x6000000
MOV R4, #0x61
ADD R4, R4, #0xc500
ADD R4, R4, #0x150000
ADD R4, R4, #0x2000000
MOV R5, #0xc8
ADD R5, R5, #0xd600
ADD R5, R5, #0xdf0000
ADD R5, R5, #0x3000000
MOV R6, #0x3
ADD R6, R6, #0x2c00
ADD R6, R6, #0x7c0000
ADD R6, R6, #0x1000000
MOV R7, #0xb7
ADD R7, R7, #0x3b00
ADD R7, R7, #0x5f0000
ADD R7, R7, #0x9000000
MOV R8, #0xdf
ADD R8, R8, #0x5500
ADD R8, R8, #0x2b0000
ADD R8, R8, #0xd000000
MOV R9, #0x81
ADD R9, R9, #0x9700
ADD R9, R9, #0x140000
ADD R9, R9, #0x8000000
MOV R10, #0x7e
ADD R10, R10, #0x5f00
ADD R10, R10, #0xc00000
ADD R10, R10, #0x3000000
MOV R11, #0x52
ADD R11, R11, #0xaf00
ADD R11, R11, #0x550000
ADD R11, R11, #0x6000000
MOV R12, #0xae
ADD R12, R12, #0xaf00
ADD R12, R12, #0xef0000
ADD R12, R12, #0x1000000
MOV R13, #0x7e
ADD R13, R13, #0x1c00
ADD R13, R13, #0x900000
ADD R13, R13, #0xc000000
MOV R14, #0x43
ADD R14, R14, #0xb400
ADD R14, R14, #0x720000
ADD R14, R14, #0x4000000


MVNSAL R5,R10,ROR R6
MOV R13, #0x1000
LDMIA R13, {R0-R12}
MOV R0, #0
              ADDMI R0, R0, #8
              ADDEQ R0, R0, #4
              ADDCS R0, R0, #2
              ADDVS R0, R0, #1
