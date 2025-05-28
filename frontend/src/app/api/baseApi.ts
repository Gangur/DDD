import {
  BaseQueryApi,
  FetchArgs,
  fetchBaseQuery,
} from "@reduxjs/toolkit/query";
import config from "../../../config.json";
import { startLoading, stopLoading } from "../layout/uiSlice";
import { toast } from "react-toastify";
import { router } from "../router/Routes";

const customBaseQuery = fetchBaseQuery({
  baseUrl: `${config.SERVER_URL}/v1/`,
  responseHandler: "content-type",
  credentials: 'include'
});

const sleep = () => new Promise((resolve) => setTimeout(resolve, 300));

export const baseQueryWirhErrorHandling = async (
  args: string | FetchArgs,
  api: BaseQueryApi,
  extraOptions: object
) => {
  api.dispatch(startLoading());
  await sleep();
  const result = await customBaseQuery(args, api, extraOptions);
  api.dispatch(stopLoading());
  if (result.error) {
    const errorDetails = result.error;
    const errorData = parseJsonOrString(errorDetails.data as string);

    switch (errorDetails.status) {
      case 400:
        if (typeof errorData === "string") toast.error(errorData);
        else if ("errors" in errorData) {
          const errors = Object.values(errorData.errors).flat().join(", ");
          toast.error(errors);
        } else {
          console.error(errorData);
        }
        break;
      case 401:
        toast.error(errorData as string);
        break;
      case 404:
        router.navigate("/not-found");
        break;
      case 500:
        try {
          router.navigate("/server-error", { state: { error: errorData } });
        } catch (error: any) {
          toast.error(error);
        }
        break;
      default:
        break;
    }
  }

  return result;
};

function parseJsonOrString(str: string) {
  try {
    return JSON.parse(str);
  } catch (e) {
    return str;
  }
}
