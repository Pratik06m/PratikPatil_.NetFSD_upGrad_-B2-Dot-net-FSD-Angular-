const products = [
  { name: "Laptop", price: 50000, quantity: 1 },
  { name: "Mouse", price: 500, quantity: 2 }
];

const calculateTotal = (items) =>
  items.reduce((total, item) => total + item.price * item.quantity, 0);

const generateInvoice = (items) => {
  const lines = items.map(
    item => `${item.name} - ₹${item.price * item.quantity}`
  ).join("\n");

  return `Invoice:\n${lines}\nTotal: ₹${calculateTotal(items)}`;
};

module.exports = { products, generateInvoice };