const axios = require('axios').default;

var client = axios.create({
  baseURL: 'http://localhost:5000',
  timeout: 1000
});

const api = {
  getBenefits: async () => {
    const result = await client.get('/Benefits');
    return result.data.benefits;
  },
  getDiscounts: async () => {
    const result = await client.get('/Discounts');
    return result.data.discounts;
  },
  getEmployees: async () => {
    const result = await client.get('/Employees');
    return result.data.employees;
  },
  getEmployeeById: async (id) => {
    const result = await client.get(`/Employees/${id}`);
    if (result.status !== 200) {
      throw new Error('Failed to get dependent.');
    }
    return result.data;
  },
  createEmployee: async (employeeData) => {
    const result = await client.post('/Employees', employeeData);
    if (result.status !== 201) {
      throw new Error('Failed to create employee.');
    }
    return result.data;
  },
  modifyEmployee: async (id, employeeData) => {
    const result = await client.put(`/Employees/${id}`, employeeData);
    if (result.status !== 202) {
      throw new Error('Failed to modify employee.')
    }
    return result.data;
  },
  getDependentById: async (employeeId, dependentId) => {
    const url = `/Employees/${employeeId}/Dependents/${dependentId}`;
    const result = await client.get(url);
    if (result.status !== 200) {
      throw new Error('Failed to get dependent.');
    }
    return result.data;
  },
  createDependent: async (employeeId, dependentData) => {
    const url = `/Employees/${employeeId}/Dependents/`;
    const request = {dependents: [dependentData]}
    const result = await client.post(url, request);
    if (result.status !== 202) {
      throw new Error('Failed to create dependent.');
    }
    return result.data;
  },
  modifyDependent: async (employeeId, dependentId, dependentData) => {
    const url = `/Employees/${employeeId}/Dependents/${dependentId}`;
    const result = await client.put(url, dependentData);
    if (result.status !== 202) {
      throw new Error('Failed to modify dependent.');
    }
    return result.data;
  },
  removeDependent: async (employeeId, dependentId) => {
    const url = `/Employees/${employeeId}/Dependents/${dependentId}`;
    const result = await client.delete(url);
    if (result.status !== 202) {
      throw new Error('Failed to delete dependent.');
    }
    return result.data;
  },
  removeEmployee: async (id) => {
    const result = await client.delete(`/Employees/${id}`);
    if (result.status !== 201) {
      throw new Error('Failed to delete employee.');
    }
    return result.data;
  },
  getEnrollments: async () => {
    const result = await client.get('/Enrollments');
    return result.data;
  },
  getEnrollmentById: async (id) => {
    const result = await client.get(`/Enrollments/${id}/Calculations`);
    if (result.status !== 200) {
      throw new Error('Failed to get enrollment.');
    }
    return result.data;
  },
  createEnrollment: async (employeeId, benefit, discounts) => {
    const payload = { employeeId, benefit, discounts };
    const result = await client.post('/Enrollments', payload);
    if (result.status !== 201) {
      throw new Error('Failed to create enrollment.');
    }
    return result.data;
  },
  modifyEnrollment: async (id, enrollmentData) => {
    const result = await client.put(`/Enrollments/${id}`, enrollmentData);
    if (result.status !== 202) {
      throw new Error('Failed to modify enrollment.')
    }
    return result.data;
  },
  removeEnrollmentById: async (id) => {
    const result = await client.delete(`/Enrollments/${id}`)
    if (result.status !== 201) {
      throw new Error('Failed to delete enrollment.');
    }
    return result.data;
  }
};

export default api;