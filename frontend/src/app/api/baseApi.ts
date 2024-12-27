import { BaseQueryApi, FetchArgs, fetchBaseQuery } from "@reduxjs/toolkit/query";
import config from "../../../config.json";

const customBaseQuery = fetchBaseQuery({
    baseUrl: `${config.SERVER_URL}/v1/`
});

const sleep = () => new Promise(resolve => setTimeout(resolve, 1000));

export const baseQueryWirhErrorHandling = async (
    args: string | FetchArgs, 
    api: BaseQueryApi, 
    extraOptions: object) => {

        await sleep();
        const result = await customBaseQuery(args, api, extraOptions);

        if (result.error){
            const {status, data} = result.error;
        }

        return result;
    }