type BinaryTree =
  | Empty
  | Node of int * BinaryTree * BinaryTree

let rec inTree tree number =
  match tree with
  | Empty -> false
  | Node (x, _, right) when x < number -> inTree right number
  | Node (x, left, _) when x > number -> inTree left number
  | Node (x, _, _) -> true

let rec addToTree tree (number:int) = 
  match tree with
  | Empty -> Node (number, Empty, Empty)
  | Node (x, left, right) when x < number -> Node (x, left, (addToTree right number))
  | Node (x, left, right) when x > number -> Node (x, (addToTree left number), right)
  | Node (x, _, _) as node -> node

let test = [3; 5; 2; 4; 8; 6; 1] |> List.fold addToTree Empty

printfn "%A" (test)
printfn "%A" (inTree test 9)
printfn "%A" ([3; 5; 2; 4; 8; 6; 1] |> List.map (inTree test))
