type Heap = int array

let swap (a: Heap) index1 index2 = 
  let temp = a.[index1]
  a.[index1] <- a.[index2]
  a.[index2] <- temp //NONFUNCTIONAL AAAAAAAAAAAH
  a

let findHigherChild heap fst_child snd_child =
  if (Array.length heap) <= fst_child then
    None
  elif Array.length heap <= snd_child || heap.[fst_child] > heap.[snd_child] then
    Some fst_child
  else
    Some snd_child

let rec heapifyUp (heap: Heap) index : Heap =
  let high_child = findHigherChild heap (index*2+1) (index*2+2)
  match high_child with
  | None -> heap  
  | Some child when heap.[child] > heap.[index] -> 
      heapifyUp (swap heap index child) child
  | _ -> heap

let rec heapifyDown (heap: Heap) index : Heap =
  let parent_index = index/2
  match heap.[index] with
  | 0 -> heap
  | x when x > heap.[parent_index] ->
    heapifyDown (swap heap index parent_index) parent_index
  | _ -> heap

let addToHeap (heap: Heap) number =
  heapifyDown (Array.append heap [|number|]) (Array.length heap) //last index with append

let heapSort (numbers: int list) =
  let rec helper (output: int list) heap =
    let last = Array.length heap - 1
    match heap with
      | [|x|] -> x::output
      | _ -> 
          let newHeap = heapifyUp (Array.append [|heap.[last]|] heap.[1 .. last-1]) 0
          helper (heap.[0]::output) newHeap

  helper [] (numbers |> List.fold addToHeap [||])


let test = heapSort [3; 5; 2; 4; 2; 2; 11; 3; 9; 19; 8; 6; 1]
printfn "%A" test
