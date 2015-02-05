type OpenNumberSequence = {
  Lower: int;
  Upper: int
} // Non-inclusive, aka (1,3) not [1,3]

type NumberSequenceOverlaps = LowerOnly | UpperOnly | Inside | Outside | NoOverlap

let findOverlap overlaper overlapee =
  match overlaper with
  | { Lower = l } when l >= overlapee.Upper -> NoOverlap
  | { Upper = u } when u <= overlapee.Lower -> NoOverlap
  | { Lower = l; Upper = u } when l <= overlapee.Lower ->
    if u < overlapee.Upper then LowerOnly else Outside
  | { Lower = l; Upper = u } when u >= overlapee.Upper -> 
    if l > overlapee.Lower then UpperOnly else Outside
  | _ -> Inside

let openSequenceUnion seq1 seq2 =
  match findOverlap seq1 seq2 with
  | LowerOnly -> Some { Lower = seq1.Lower; Upper = seq2.Upper }
  | UpperOnly -> Some { Lower = seq2.Lower; Upper = seq1.Upper }
  | Outside -> Some seq1
  | Inside -> Some seq2
  | NoOverlap -> None

let rec addSequenceToSequenceList sequences toAdd =
  let rec helper tested toTest toAdd = 
    match toTest with
    | [] -> toAdd::tested
    | head::rest -> 
        match openSequenceUnion toAdd head with
        | Some sequence -> addSequenceToSequenceList (tested @ rest) sequence
        | None -> helper (head::tested) rest toAdd
  helper [] sequences toAdd

let convertNumbersToSequenceList numberList =
  numberList 
      |> List.map (fun x -> { Lower = x-1; Upper = x+1 })
      |> List.fold addSequenceToSequenceList []

let countSequences numberList =
  numberList |> convertNumbersToSequenceList |> List.length

printf "%A" (countSequences [1; 3; 5; 6; 7; 2; 10])
