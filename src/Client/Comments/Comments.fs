module Client.Comments

open Feliz
open Feliz.Bulma


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


