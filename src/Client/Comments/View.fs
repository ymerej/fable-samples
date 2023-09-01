module Client.Comments.View

open Feliz
open Feliz.Bulma

let renderCommentVariations (comments : string list) commentProps =
    [
        Bulma.container [
            BulmaCommentBox { OnAddComment = commentProps.OnAddComment  }
        ]

        EmbeddedCommentBox { OnAddComment = commentProps.OnAddComment  }

        Bulma.container [
            Html.h1 [ Html.text "Comments" ]
            Html.ul [
                for comment in comments do
                    Html.li [
                        Html.p comment
                    ]
            ]
        ]
    ]
