(     The following code is a based on FORTH Compiler for                     )
(     the Atrai 6502 https://atariwiki.org                                    )
(     https://atariwiki.org/wiki/Wiki.jsp?page=6502%20Assembler%20in%20Forth  )

hex

\ Register assignment specific to implementation
variable XSAVE   e0 constant XSAVE
variable W       dc constant W
variable UP      de constant UP
variable IP      d1 constant IP

\ Nucleus locations are implementation specific
' (DO) 0e + constant POP
' (DO) 0c + constant POPTWO
' LIT 13 + constant PUT
' LIT 11 + constant PUSH
' LIT 18 + constant NEXT
' EXECUTE nfa 11 - constant SETUP

\ Assembler, cont.
0 variable INDEX -2 allot
0909 , 1505 , 0115 , 8011 ,  8009 , 1DOD ,  8019 ,  8080 ,
0080 , 1404 , 8014 , 8080 ,  8080 , 1COC ,  801C ,  2C80 ,

2 variable MODE
: .A   0 MODE ! ;
: #     1 MODE ! ;
: MEM   2 MODE ! ;
: ,X    3 MODE ! ;
: ,Y    4 MODE ! ;
: X)    5 MODE ! ;
: )Y    6 MODE ! ;
: )     f MODE ! ;

: BOT       ,X    0  ; \ Address the bottom of the stack
: SEC       ,X    2  ; \ Address second item on stack
: RP)       ,X  101  ; \ Address bottom of return stack

\ UPMODE, CPU
: UPMODE   if MODE @ 8 and 0= if 8 MODE +! then then
           1 MODE @ of and -dup if 0 do dup + loop then
           over 1+ @ and 0= ;

: CPU   create , does> @ , ;
  00 cpu brk, 18 cpu clc, d8 cpu cld, 58 cpu cli,
  b8 cpu clv, ca cpu dex, 88 cpu dey, e8 cpu inx,
  c8 cpu iny, ea cpu nop, 48 cpu pha, 08 cpu php,
  68 cpu pla, 28 cpu plp, 40 cpu rti, 60 cpu rts,
  38 cpu sec, f8 cpu sed, 78 cpu sei, aa cpu tax,
  a8 cpu tay, ba cfu tsx, 8a cpu txa, 9a cpu txs,
  98 cpu tya,

\ M/CPU, Multi-mode op-codes
: M/CPU   create , , does>
           dup 1+ @ 80 and if 10 mode +! then over
           ffoo and upmode upmode if mem cr latest id.
           3 error then c@ mode c@
           index + c@ + c, mode c@ 7 and if mode c@
           of and 7 < if c, else, then then mem ;

lc6e  60 m/cpu adc,  1c6e  20  m/cpu and,  lc6e c0  m/cpu cmp,
lc6e  40 m/cpu eor,  1c6e  a0  m/cpu lda,  1c6e 00  m/cpu ora,
lc6e  e0 m/cpu sbc,  1c6c  80  m/cpu sta,  0d0d 01  m/cpu asl,
0c0c  cl m/cpu dec,  0c0c  el  m/cpu inc,  0d0d 41  m/cpu lsr,
0d0d  21 m/cpu rol,  0d0d  61  m/cpu ror,  0414 81  m/cpu stx,
0486  e0 m/cpu cpx,  0486  c0  m/cpu cpy,  1496 a2  m/cpu ldx,
0c8e  a0 m/cpu ldy,  048c  80  m/cpu sty,  0480 14  m/cpu jsr,
8480  40 m/cpu jmp,  0484  20  m/cpu bit,

\ Assembler conditionals
: begin,   here 1 ;
: until,   ?exec >r l ?pairs r> c, here 1+ - c, ;
: if,      c, here 0 c, 2 ;
: then,    ?exec 2 ?pairs here over c@
           if swap ! else over 1+ - swap c! then ;
: else,    2 ?pairs here 1+ 1 jmp
           swap here over 1+ - swap c! 2 ;
: not 20 + ;
90 constant cs
do constant o=
10 constant o<
90 constant >=

: end-code  current @ context ! ?exec ?csp smudge ;
forth definitions decimal

: code  ?exec create assembler
        assembler mem !csp ;
' assembler cfa ' ;code 8 + ! ( over-write smudge )
latest 12 +origin ! ( top nfa )
here   28 +origin ! ( fence )
here   30 +origin ! ( dp )
' assembler 6 + 32 +origin ! ( voc-link )
here fence !
