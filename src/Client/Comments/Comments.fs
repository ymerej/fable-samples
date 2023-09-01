module Client.Comments

open Feliz
open Feliz.Bulma
open Feliz.style

// This is some example variations of how ReactComponents can be written.
//   This is not an exhaustive list.
// Within the realm of styling and HTML generation there are many syntax variations possible.
//      Anywhere from customizing on a per line of code basis
//      to levels of composition along the lines of a design library


type CommentBoxProps = {
    // Callback for when use clicks the Add Comment button,
    //   this same callback event is used by all variations
    OnAddComment: string -> unit
}


[<ReactComponent>]
let BulmaCommentBox (props: CommentBoxProps) =
    // The first variation uses a React Component.
    // React reference: https://react.dev/learn/your-first-component

    // Because this is a component, it should be possible to export the component
    //      into an NPM package, later I will prove that out.

    // Internal state of comment as it's being typed using a hook.
    let (comment, setComment) = React.useState ""

    Html.div [
        // The strongly typed styling ensure that elements are created correctly using the correct class names
        // [Bulma](https://bulma.io) is a CSS library.
        // The strongly typed Bulma nuget comes is [Bulma.Feliz](https://www.nuget.org/packages/Feliz.Bulma)
        // There are Feliz style CSS DSLs for MaterialUI and Tailwind.  But not Bootstrap.  Perhaps Bootstrap has fallen out of favor.
        Bulma.field.p [
            Bulma.control.p [
                Bulma.textarea [
                    prop.placeholder "Enter your comment...";
                    prop.value comment;
                    prop.onChange (fun (ev: string) -> setComment ev)

                ]
            ]
        ]
        Bulma.field.p [
            Bulma.control.p [
                Bulma.button.button [
                    prop.onClick (fun _ ->
                        props.OnAddComment comment
                        setComment ""
                    );
                    prop.text "Add Comment"
                    prop.disabled (comment.Length < 1)
                ]
            ]
        ]
    ]


[<ReactComponent>]
let EmbeddedCommentBox (props: CommentBoxProps) =
    // This version has styles embedded using Feliz style DSL

    let (comment, setComment) = React.useState ""
    Html.div [
        prop.style [
            style.margin  (length.px 20, length.px 0)
        ]
        prop.children [
            Html.div [
                prop.children [
                    Html.textarea [
                        prop.placeholder "Enter your comment...";
                        prop.value comment;
                        prop.onChange (fun (ev: string) -> setComment ev);
                        prop.style [
                            // Feliz offers strongly typed styles to help ensure that the styles are verified by the compiler
                            //   as opposed to verifying everything within a browser
                            style.width (length.perc 100.);
                            style.padding (length.px 10);
                            style.borderWidth 1;
                            borderStyle.solid;
                            borderColor.lightGray;
                            style.borderRadius (length.px 5)
                        ]
                    ]
                ]
            ]
            Html.div [
                Html.button [
                    prop.onClick (fun _ ->
                        props.OnAddComment comment
                        setComment ""
                    )

                    prop.text "Add Comment"

                    // F# natively uses List Comprehension.
                    //   In C# this is closely related to code interspersed code within StringBuilder.Append.
                    // In the case below, color styles are grouped into an array and then output later with a yield!.
                    let colorStyles =
                        [
                            // NOTE: the choice of combining color styles is arbitrary.
                            //   Any set of styles could be created, anywhere from local code to global.
                            backgroundColor.blue; // trailing semicolons are optional
                            style.color.white
                            borderColor.lightGray
                        ]

                    prop.style [
                        yield! colorStyles
                        style.padding (length.px 10, length.px 20);
                        style.borderWidth 1;
                        borderStyle.solid;
                        style.borderRadius (length.px 5);
                        cursor.pointer
                    ]
                ]
            ]
        ]
    ]

[<ReactComponent>]
let EmbeddedRawCommentBox (props: CommentBoxProps) =
    let (comment, setComment) = React.useState ""
    Html.div [
        prop.children [
            Html.style [
                // It's also possible to get all the way down to raw CSS.
                //   This is not preferred, as it could potentially introduce dangerous code, see dangerouslySetInnerHTML below.
                //   This is also against preferred means of doing styles with React
                prop.dangerouslySetInnerHTML """
                .commentBox {
                    margin: 20px 0px;
                }
                .commentBox .commentArea {
                    margin-bottom: 10px;
                }
                .commentBox .commentArea textarea {
                    width: 100%;
                    padding: 10px;
                    border: 1px solid #ccc;
                    border-radius: 5px;
                }
                .commentBox button {
                    background-color: #007bff;
                    color: white;
                    padding: 10px 20px;
                    border: none;
                    border-radius: 5px;
                    cursor: pointer;
                }
                """
            ]
            Html.div [
                prop.className "commentBox"
                prop.children [
                    Html.div [
                        prop.className "commentArea"
                        prop.children [
                            Html.textarea [
                                prop.placeholder "Enter your comment...";
                                prop.value comment;
                                prop.onChange (fun (ev: string) -> setComment ev);
                            ]
                        ]
                    ]
                    Html.div [
                        prop.children [
                            Html.button [
                                prop.onClick (fun _ ->
                                    props.OnAddComment comment
                                    setComment ""
                                );
                                prop.text "Add Comment"
                            ]
                        ]
                    ]
                ]
            ]
        ]
    ]
