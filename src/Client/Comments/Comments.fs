module Client.Comments

open Feliz
open Feliz.Bulma
open Feliz.Styles
open Feliz.style



type CommentBoxProps = {
    // Callback for when use clicks the Add Comment button
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
            // prop.style [
            //     style.marginBottom (length.px 10)
            // ]
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
                prop.style [
                    backgroundColor.blue;
                    style.color.white;
                    style.padding (length.px 10, length.px 20);
                    style.borderWidth 1;
                    borderStyle.solid;
                    borderColor.lightGray;
                    style.borderRadius (length.px 5);
                    cursor.pointer
                ]
            ]
        ]
        ]
    ]

