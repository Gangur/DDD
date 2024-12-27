import Cookies from "universal-cookie";
import agent from "../app/api/agent";
import { store } from "../app/store/configureStore";

const cookies = new Cookies();
function setCookie(key: string, value: string | number) {
  cookies.set(key, value, { path: "/" });
}

export function getCookie(key: string) {
  return cookies.get(key);
}

const customerKey = "customer-id";
const orderKey = "order-id";

export async function getCustomerIdAsync() {
  // eslint-disable-next-line prefer-const
  let customerId = cookies.get(customerKey);

  if (!customerId || customerId == "undefined") {
    if (!store.getState().account.user) {
      const customerId = await agent.customeres.create();
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
