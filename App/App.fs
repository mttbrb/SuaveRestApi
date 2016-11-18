namespace SuaveRestApi

module App =
    open SuaveRestApi.Rest
    open SuaveRestApi.Db
    open Suave
    open Suave.Web

    [<EntryPoint>]
    let main argv =
        let personWebPart = rest "people" {
            GetAll = Db.getPeople
        }
        startWebServer defaultConfig personWebPart
        0 // return an integer exit code
