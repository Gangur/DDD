import Cookies from "universal-cookie";
import agent from "../app/api/agent";

const cookies = new Cookies();
function setCookie(key: string, value: string | number) {
  cookies.set(key, value, { path: "/" });
}

const customerKey = "customer-id";
const orderKey = "order-id";

export async function getCustomerIdAsync() {
  // eslint-disable-next-line prefer-const
  let customerId = cookies.get(customerKey);

  if (!customerId || customerId == "undefined") {
    const customerId = await agent.customeres.create();
    setCookie(customerKey, customerId);

    const orderId = await agent.orders.create(customerId);
    setCookie(orderKey, orderId);
  }

  return customerId;
}
