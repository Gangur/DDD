import axios, { AxiosError, AxiosResponse } from "axios";
import config from "../../../config.json";
import { AuthClient, BuggyClient, CustomeresClient, FilesClient, OrdersClient, ProductsClient } from "./http-client";
import { toast } from "react-toastify";
import { router } from "../router/Routes";
import { store } from "../store/configureStore";

const onResponse = (response: AxiosResponse): AxiosResponse<any, any> => {
  return response;
};

const onResponseError = (error: AxiosError): Promise<AxiosError> => {
    const status = error.status;
    const errorMessage = error.message;
    const apiErrorMessage = error.response?.data;

    switch (status) {
        case 400:
          if (status) {
            try {
              const modelStateErrors: string[] = [];
              const data = JSON.parse(apiErrorMessage as string || errorMessage);
              for (const key in data.errors) {
                modelStateErrors.push(data.errors[key]);
              }

              throw modelStateErrors;
            } catch (errors: any) {
              toast.error(errors.join(' '));
              throw errors;
            }
          }
          break;
        case 401:
          toast.error(apiErrorMessage as string || errorMessage);
          break;
        case 404:
          break;
        case 500:
          try {
            const error = JSON.parse(errorMessage);
            router.navigate("/server-error", { state: { error: error } });
          } catch (error: any) {
            toast.error(error);
          }
          break;
        default:
          break;
      }

  return Promise.reject(error);
};

const instance = axios.create({
  baseURL: config.SERVER_URL,
  transformResponse: (data) => data,
  withCredentials: true
});

instance.defaults.baseURL = config.SERVER_URL;
instance.interceptors.response.use(onResponse, onResponseError);
instance.interceptors.request.use(config => {
  const token = store.getState().account.user?.token;
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
})

const auth = new AuthClient(config.SERVER_URL, instance);
const buggy = new BuggyClient(config.SERVER_URL, instance);
const customeres = new CustomeresClient(config.SERVER_URL, instance);
const files = new FilesClient(config.SERVER_URL, instance);
const orders = new OrdersClient(config.SERVER_URL, instance);
const products = new ProductsClient(config.SERVER_URL, instance);

export default { auth, buggy, customeres, files, orders, products }
