module Client.FormControls.Person

open Fable.Core.JsInterop
open System
open Fable.React
open Fable.React.Props
open Elmish

type Model = {
    FirstName: string
    LastName: string
    Age: string
    Errors: Map<string, string>
}

type Msg =
    | SetFirstName of string
    | SetLastName of string
    | SetAge of string
    | ValidateForm

let initModel = {
    FirstName = ""
    LastName = ""
    Age = ""
    Errors = Map.empty
}

let validateField key value errors =
    match key with
    | "FirstName" | "LastName" when String.IsNullOrWhiteSpace(value) -> errors.Add(key, "Required field")
    | "Age" when not (System.Int32.TryParse(value, (fun _ -> ()))) -> errors.Add(key, "Age must be a number")
    | _ -> errors

let update msg model =
    let newErrors =
        match msg with
        | SetFirstName v -> validateField "FirstName" v model.Errors
        | SetLastName v -> validateField "LastName" v model.Errors
        | SetAge v -> validateField "Age" v model.Errors
        | ValidateForm ->
            ["FirstName"; "LastName"; "Age"]
            |> List.fold (fun acc key -> validateField key (keyValue key model) acc) model.Errors
    match msg with
    | SetFirstName v -> { model with FirstName = v; Errors = newErrors }, Cmd.none
    | SetLastName v -> { model with LastName = v; Errors = newErrors }, Cmd.none
    | SetAge v -> { model with Age = v; Errors = newErrors }, Cmd.none
    | ValidateForm when newErrors.IsEmpty -> model, Cmd.ofMsg (printfn "Form submitted successfully!") // handle form submission
    | _ -> model, Cmd.none

let view model dispatch =
    div [] [
        div [] [
            label [] [str "First Name:"]
            input [ Type "text"; Value model.FirstName; OnChange (fun e -> dispatch (SetFirstName e.Value)) ]
            match model.Errors.TryFind "FirstName" with
            | Some err -> div [ ClassName "error" ] [ str err ]
            | None -> Fable.React.Empty // React.nullElement
        ]

        div [] [
            label [] [str "Last Name:"]
            input [ Type "text"; Value model.LastName; OnChange (fun e -> dispatch (SetLastName e.Value)) ]
            match model.Errors.TryFind "LastName" with
            | Some err -> div [ ClassName "error" ] [ str err ]
            | None -> Empty
        ]

        div [] [
            label [] [str "Age:"]
            input [ Type "text"; Value model.Age; OnChange (fun e -> dispatch (SetAge e.Value)) ]
            match model.Errors.TryFind "Age" with
            | Some err -> div [ ClassName "error" ] [ str err ]
            | None -> Empty
        ]

        button [ OnClick (fun _ -> dispatch ValidateForm) ] [ str "Submit" ]
    ]

