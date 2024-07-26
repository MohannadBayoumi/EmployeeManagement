<template>
    <div>
        <h2>Add New Employee</h2>
        <form @submit.prevent="handleSubmit">
            <div class="form-group">
                <label for="image">Image</label>
                <!-- <input type="file" @change="onFileChange" accept="image/*"> -->
                <div class="image-container">
                    <input type="file" @change="onFileChange" accept="image/*" class="file-input">
                    <img v-if="emp.image" :src="emp.image" alt="Uploaded Image" class="circular-image">
                    <div v-else class="placeholder">
                        <i class="fas fa-camera"></i>
                    </div>
                    <div class="upload-icon" v-if="emp.image">
                        <i class="fas fa-camera"></i>
                    </div>
                </div>
                <div v-if="submitted && errors.has('image')" class="invalid-feedback">{{ errors.first('image') }}</div>
            </div>
            <div class="form-group">
                <label for="fullName">Full Name</label>
                <input type="text" v-model="emp.fullName" v-validate="'required'" name="fullName" class="form-control" :class="{ 'is-invalid': submitted && errors.has('fullName') }" />
                <div v-if="submitted && errors.has('fullName')" class="invalid-feedback">{{ errors.first('fullName') }}</div>
            </div>
            <div class="form-group">
                <label for="email">Email</label>
                <input type="text" v-model="emp.email" v-validate="'required'" name="email" class="form-control" :class="{ 'is-invalid': submitted && errors.has('email') }" />
                <div v-if="submitted && errors.has('email')" class="invalid-feedback">{{ errors.first('email') }}</div>
            </div>
            <div class="form-group">
                <label for="phoneNumber">Phone Number</label>
                <input type="text" v-model="emp.phoneNumber" v-validate="'required'" name="phoneNumber" class="form-control" :class="{ 'is-invalid': submitted && errors.has('phoneNumber') }" />
                <div v-if="submitted && errors.has('phoneNumber')" class="invalid-feedback">{{ errors.first('phoneNumber') }}</div>
            </div>
            <div class="form-group">
                <label for="graduateDegree">Gradute Degree</label>
                <select v-model="emp.graduateDegree" v-validate="{ required: true }" name="graduateDegree" class="form-control" :class="{ 'is-invalid': submitted && errors.has('graduateDegree') }">
                    <option value="null" disabled selected>Select your option</option>  
                    <option value=1>Graduate</option>
                    <option value=2>Under Graduate</option>
                </select>
                <div v-if="submitted && errors.has('graduateDegree')" class="invalid-feedback">{{ errors.first('graduateDegree') }}</div>
            </div>
            <div class="form-group">
                <button class="btn btn-primary" :disabled="submitting">{{ emp.id ? 'Edit' : 'Add' }}</button>
                <!-- <img v-show="status.registering" src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA==" /> -->
                <router-link to="/" class="btn btn-link">Cancel</router-link>
            </div>
        </form>
    </div>
</template>

<script>
import { mapState, mapActions } from 'vuex'

export default {
    data () {
        return {
            userId: null,
            progress: 0,
            error: '',
            submitted: false
        }
    },
    mounted() {
        this.userId = +this.$route.query.id;
        this.getEmployee(this.userId);
    },
    computed: {
        ...mapState('users', ['emp', 'submitting'])
    },
    methods: {
        ...mapActions('users', ['addEditEmployee', 'getById']),

        getEmployee(id) {
            this.getById(id);
        },

        handleSubmit(e) {
            this.submitted = true;
            this.$validator.validate().then(valid => {
                if (valid) {
                    this.addEditEmployee(this.emp);
                }
            });
        },

        onFileChange(event) {
            const file = event.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = () => {
                    this.emp.image = reader.result; // Set the Base64 string
                };
                reader.onerror = (error) => {
                    console.error('Error reading file:', error);
                }
            }
        }
    }
};
</script>

<style scoped>
.image-container {
  position: relative;
  width: 150px;
  height: 150px;
  margin: 20px auto;
  text-align: center;
}

.file-input {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  opacity: 0;
  cursor: pointer;
}

.circular-image {
  width: 100%;
  height: 100%;
  border-radius: 50%;
  object-fit: cover;
  border: 2px solid #ccc;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.placeholder {
  width: 100%;
  height: 100%;
  border-radius: 50%;
  background-color: #f0f0f0;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 24px;
  color: #ccc;
  border: 2px solid #ccc;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.upload-icon {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  font-size: 24px;
  color: white;
  background-color: rgba(0, 0, 0, 0.5);
  border-radius: 50%;
  padding: 10px;
  display: none;
}

.image-container:hover .upload-icon {
  display: block;
}
</style>
