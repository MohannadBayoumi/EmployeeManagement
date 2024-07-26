<template>
    <div class="user-table">
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Email</th>
            <th>Phone Number</th>
            <th>Degree</th>
            <th v-if="role == 'Admin'">Action</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="user in users" :key="user.id">
            <td>{{ user.fullName }}</td>
            <td>{{ user.email }}</td>
            <td>{{ user.phoneNumber }}</td>
            <td>{{ graduateDegrees[user.graduateDegree - 1] }}</td>
            <td v-if="role == 'Admin'">
              <span><router-link :to="getRegisterLink(user.id)" class="btn btn-link">Edit Employee</router-link></span>
              <span><a @click="deleteUser(user.id)" class="btn btn-link text-danger">Delete</a></span>
            </td>
          </tr>
        </tbody>
      </table>
      <div class="mt-2">
          <button @click="logout" class="btn btn-primary">Logout</button>
          <router-link to="/register?id=null" class="btn btn-link">Add new employee</router-link>
      </div>
    </div>
  </template>
  
  <script>
    import { mapState, mapActions } from 'vuex'

    export default {
        data() {
          return {
            graduateDegrees: ['Graduate', 'Undergrad'],
            role: ''
          }
        },
        computed: {
            ...mapState({
                // account: state => state.account,
                users: state => state.users.all.items
            })
        },
        mounted() {
          this.role = JSON.parse(localStorage.getItem('user')).role;
        },
        created () {
            this.getAllUsers();
        },
        methods: {
            ...mapActions('users', {
                getAllUsers: 'getAll',
                deleteUser: 'delete',
                logout: 'logout',
                editEmployee: 'editEmployee'
            }),
            getRegisterLink(id) {
                return { path: '/register', query: { id: id } }; 
            }
        }
    };
</script>
  
  <style scoped>
  /* Add your custom styles here */
  .user-table {
    font-family: Arial, sans-serif;
    border-collapse: collapse;
    width: 100%;
  }
  
  .user-table th,
  .user-table td {
    border: 1px solid #ddd;
    padding: 8px;
    text-align: left;
  }
  
  .user-table th {
    background-color: #f2f2f2;
  }
  
  .user-table tr:nth-child(even) {
    background-color: #f2f2f2;
  }
  
  .text-danger {
    color: red;
  }

  .logout-button {
    color: blue;
    border: none;
    padding: 10px 20px;
    font-size: 16px;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s ease;
    }

    .logout-button:hover {
    }
  </style>
  