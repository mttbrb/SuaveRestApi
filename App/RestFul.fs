namespace SuaveRestApi.Rest

open Newtonsoft.Json
open Newtonsoft.Json.Serialization
open Suave 
open Suave.Operators
open Suave.Http
open Suave.Successful

[<AutoOpen>]
module RestFul = 
    open Suave.RequestErrors
    open Suave.Filters

    let JSON v =
        let settings = new JsonSerializerSettings()
        settings.ContractResolver <- new CamelCasePropertyNamesContractResolver()
        JsonConvert.SerializeObject(v, settings)
        |> OK 
        >=> Writers.setMimeType "application/json; charset=utf-8"

    type RestResource<'a> = {
        GetAll : unit -> 'a seq
    }

    let rest resourceName resource =
        let resourcePath = "/" + resourceName
        let getAll = warbler (fun _ -> resource.GetAll () |> JSON)
        path resourcePath >=> GET >=> getAll


    