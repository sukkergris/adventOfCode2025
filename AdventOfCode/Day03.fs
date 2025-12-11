namespace AdventOfCode

module Lobby =
    let input01 = [|
                                "987654321111111"
                                "811111111111119"
                                "234234234234278"
                                "818181911112111"
                                |]
    let strArr2NumArr (strArr: string) : int array = strArr |> Array.ofSeq |> Array.map(fun x -> int x - int '0')
    let maxOf current next =
        if current > next then
            current
        else
            next
    let maxOfExcept (current:int) (next: int) (except:int) : int =
        if current > next && current <> except then
            current
        else
            next
    let selectMax m t =
        if m > t  then t else m

    let max (arr: int array) =
        let max : int = arr |> Array.max
        printfn "max: %d" max
        let tailMax = arr[.. arr.Length - 2] |> Array.max
        let mx = selectMax max tailMax

        let mbx : int = arr |> Array.fold (fun acc x-> maxOfExcept acc x mx) 0
        mx * 10 + mbx

    let answer = input01
                                |> Array.map (fun x -> strArr2NumArr x)
                                |> Array.map (fun x -> max x)
