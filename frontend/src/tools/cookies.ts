import Cookies from "universal-cookie";
import agent from "../app/api/agent";
import { store } from "../app/store/configureStore";

const cookies = new Cookies();

const customerKey = "customer-id";
const orderKey = "order-id";

function setCookie(key: string, value: string | number) {
  cookies.set(key, value, { path: "/" });
}

export function getCookie(key: string) {
  return cookies.get(key);
}

export async function ensureIDsExistence() {
  // eslint-disable-next-line prefer-const
  let customerId = cookies.get(customerKey);

  if (!customerId || customerId == "undefined") {
    if (!store.getState().account.user) {
      customerId = await agent.customeres.create();
      setCookie(customerKey, customerId);

      const orderId = await agent.orders.create(customerId);
      setCookie(orderKey, orderId);
    }
    else {
      const order = await agent.orders.byUser();
      setCookie(customerKey, order.customerId);
      setCookie(orderKey, order.id);
    }
  }

  return customerId;
}

export function getCookieCustomerId() {
  return cookies.get(customerKey);
}

export function getCookieOrderId() {
  return cookies.get(orderKey);
}