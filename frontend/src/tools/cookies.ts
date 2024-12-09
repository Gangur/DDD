import Cookies from "universal-cookie";
import agent from "../app/api/agent";

const cookies = new Cookies();
function setCookie(key: string, value: string | number) {
  cookies.set(key, value, { path: "/" });
}

const customerKey = "customer-id";
const orderKey = "order-id";

export function getCustomerId() {
  // eslint-disable-next-line prefer-const
  let customerId = cookies.get(customerKey);

  if (!customerId || customerId == "undefined") {
    agent.customeres.create().then((customerId) => {
      setCookie(customerKey, customerId);
      agent
        .orders.create(customerId)
        .then((orderId) => setCookie(orderKey, orderId));
    });
  }

  return customerId;
}
