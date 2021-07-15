
const formatter = new Intl.NumberFormat('en-US', {
  style: 'currency',
  currency: 'USD'
});

const handleChange = (hook) => (event) => hook(event.target.value);

export { formatter, handleChange };