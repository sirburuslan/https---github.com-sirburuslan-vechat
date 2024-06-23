<script lang="ts" setup>
// System Utils
import { ref } from 'vue';

// Installed Utils
import { useI18n } from 'vue-i18n';
import { useVuelidate } from '@vuelidate/core';
import {
  required,
  email,
  minLength,
  maxLength,
  helpers
} from '@vuelidate/validators';

// Get i18n functions
const { t } = useI18n();

// Set the page name
useHead({
  title: t('users')
});

// Get the show notification plugin
const { $showNotification } = useNuxtApp();

// Gets the users store
const usersStore = useUsersStore();

// Modal status
const showModal = ref(false);

// Create User Form Messages
const message = reactive({
  firstName: '',
  lastName: '',
  email: '',
  password: ''
});

// Rules for Vuelidate's validations
const rules = computed(() => ({
  firstName: {
    required: helpers.withMessage(t('first_name_too_short'), required),
    minLength: helpers.withMessage(t('first_name_too_short'), minLength(0)),
    maxLength: helpers.withMessage(t('first_name_too_short'), maxLength(50))
  },
  lastName: {
    required: helpers.withMessage(t('last_name_too_short'), required),
    minLength: helpers.withMessage(t('last_name_too_short'), minLength(0)),
    maxLength: helpers.withMessage(t('last_name_too_long'), maxLength(50))
  },
  email: {
    required: helpers.withMessage(t('auth_email_short'), required),
    email: helpers.withMessage(t('auth_email_not_valid'), email)
  },
  password: {
    required: helpers.withMessage(t('auth_password_short'), required),
    minLength: helpers.withMessage(t('auth_password_short'), minLength(8)),
    maxLength: helpers.withMessage(t('auth_password_long'), maxLength(20))
  }
}));

// Form Inputs
const state = reactive({
  firstName: '',
  lastName: '',
  email: '',
  password: '',
});

// Combine valdation rules with inputs value
const v$ = useVuelidate(rules, state);

// Handle the form submit
const handleSubmit = async () => {

  // Reset the error messages
  Object.assign(message, {
    firstName: '',
    lastName: '',
    email: '',
    password: ''
  });

  // Trigger validation
  v$.value.$touch();

  // Verify if errors exists
  if (v$.value.$invalid) {
    // List the errors
    v$.value.$silentErrors.map((error) => {
      // Set error mesage by type
      if (error.$property == 'firstName') {
        message.firstName = error.$message;
      } else if (error.$property == 'lastName') {
        message.lastName = error.$message;
      } else if (error.$property == 'email') {
        message.email = error.$message;
      } else {
        message.password = error.$message;
      }
    });

    return;
  }

  // Create new user
  const response = await usersStore.createUser(state);

  // Check if the response is success
  if (response.success) {
      // Show success notification
      $showNotification('success', sanitizeInput(response.message));
      // Reset the state
      Object.assign(state, {
        firstName: '',
        lastName: '',
        email: '',
        password: ''
      });
      v$.value.$reset();
  } else {
    // Show error notification
    $showNotification('error', response.message?sanitizeInput(response.message):t('an_error_has_occurred'));
  }

}

// Get the data from the users store
const users = computed(() => usersStore.users);
const page = computed(() => usersStore.page);
const total = computed(() => usersStore.total);
const search = computed(() => usersStore.search);
const time = computed(() => usersStore.time);

// Run code when the component is mounted
onMounted(() => {
  if ( users.value == null ) {
    usersStore.getUsers(page.value, search.value);
  }
});

const exportUsers = () => {

  // Check if users exists
  if ( users.value && users.value.length > 0 ) {

    // Set the list
    let list: [string[]] = [[
      '"' + t('user_id') + '"',
      '"' + t('first_name') + '"',
      '"' + t('last_name') + '"',
      '"' + t('email') + '"'
    ]];

    // Total number of users
    const usersTotal: number = users.value.length;

    // List all numbers
    for ( let u = 0; u < usersTotal; u++ ) {

        // Append to list
        list.push([
          '"' + userIdFromObjectId(users.value[u].userId) + '"',
          '"' + sanitizeInput(users.value[u].firstName) + '"',
          '"' + sanitizeInput(users.value[u].lastName) + '"',
          '"' + sanitizeInput(users.value[u].email) + '"'
        ]);

    }

    // CSV variable
    let csv: string = '';

    // Prepare the csv
    list!.forEach(function(row) {
        csv += row.join(',');
        csv += "\n";
    });

    // Create the CSV link and download the file
    const csv_link: HTMLAnchorElement = document.createElement('a');

    // Set charset
    csv_link.href = 'data:text/csv;charset=utf-8,' + encodeURI(csv);

    // Open in new tab the file
    csv_link.target = '_blank';

    // Set the name of the file
    csv_link.download = 'members.csv';

    // Download the CSV
    csv_link.click();

  } else {
    // Show error notification
    $showNotification('error', t('new_users_were_found'));
  }

}
</script>
<template>
  <div class="vc-users-container">
    <div class="flex mb-3">
      <div class="w-full">
        <h2 class="vc-page-title">
          {{ $t('users') }}
        </h2>
      </div>
    </div>
    <div class="flex mb-3">
      <div class="w-full flex">
        <div class="flex vc-search-box vc-transparent-color">
          <span class="vc-search-icon">
            <Icon name="search" />
          </span>
          <input
            type="text"
            :placeholder="$t('search_for_users')"
            class="form-control vc-search-input"
            id="vc-search-for-users"
          />
          <a href="#">
            <Icon name="autorenew" extraClass="vc-load-more-icon" />
            <Icon name="cancel" extraClass="vc-cancel-icon" />
          </a>
        </div>
        <button
          class="ec-search-btn vc-new-user-button"
          @click="showModal = true"
        >
          <Icon name="person_add_alt" />
          {{ $t('new_user') }}
        </button>
        <button
          class="ec-search-btn vc-export-users-button"
          @click="exportUsers()"
        >
          <Icon name="cloud_download" />
          {{ $t('export') }}
        </button>
      </div>
    </div>
    <div class="flex mb-3">
      <div>
        <Modal :title="$t('new_user')" v-model:visible="showModal">
          <form @submit.prevent="handleSubmit">
            <div class="col-span-full vc-modal-text-input">
              <input
                type="text"
                :placeholder="$t('enter_first_name')"
                name="vc-modal-text-input-first-name"
                id="vc-modal-text-input-first-name"
                class="block px-2.5 pb-2.5 pt-4 w-full vc-modal-form-input"
                v-model="state.firstName"
                @blur="v$.firstName.$touch()"
              />
              <label for="vc-modal-text-input-first-name" class="absolute">
                <Icon name="person" />
              </label>
              <div class="vc-modal-form-input-error-message">
                {{
                  v$.firstName.$error ? v$.firstName.$errors[0].$message : ''
                }}
              </div>
            </div>
            <div class="col-span-full vc-modal-text-input">
              <input
                type="text"
                :placeholder="$t('enter_last_name')"
                name="vc-modal-text-input-last-name"
                id="vc-modal-text-input-last-name"
                class="block px-2.5 pb-2.5 pt-4 w-full vc-modal-form-input"
                v-model="state.lastName"
                @blur="v$.lastName.$touch()"
              />
              <label for="vc-modal-text-input-last-name" class="absolute">
                <Icon name="person" />
              </label>
              <div class="vc-modal-form-input-error-message">
                {{ v$.lastName.$error ? v$.lastName.$errors[0].$message : '' }}
              </div>
            </div>
            <div class="col-span-full vc-modal-text-input">
              <input
                type="email"
                :placeholder="$t('enter_email_address')"
                name="vc-modal-text-input-email"
                id="vc-modal-text-input-email"
                class="block px-2.5 pb-2.5 pt-4 w-full vc-modal-form-input"
                v-model="state.email"
                @blur="v$.email.$touch()"
              />
              <label for="vc-modal-text-input-email" class="absolute">
                <Icon name="alternate_email" />
              </label>
              <div class="vc-modal-form-input-error-message">
                {{ v$.email.$error ? v$.email.$errors[0].$message : '' }}
              </div>
            </div>
            <div class="col-span-full vc-modal-text-input">
              <input
                type="password"
                :placeholder="$t('enter_password')"
                name="vc-modal-text-input-password"
                id="vc-modal-text-input-password"
                class="block px-2.5 pb-2.5 pt-4 w-full vc-modal-form-input"
                v-model="state.password"
                @blur="v$.password.$touch()"
              />
              <label for="vc-modal-text-input-password" class="absolute">
                <Icon name="vpn_key" />
              </label>
              <div class="vc-modal-form-input-error-message">
                {{ v$.password.$error ? v$.password.$errors[0].$message : '' }}
              </div>
            </div>
            <div class="col-span-full vc-modal-button">
              <div class="text-right">
                <button
                  type="submit"
                  class="mb-3 flex justify-between vc-submit-button"
                >
                  {{ $t('save_user') }}
                  <Icon name="autorenew" extraClass="vc-load-more-icon" />
                  <Icon name="arrow_forward" extraClass="vc-next-icon" />
                </button>
              </div>
            </div>
          </form>
        </Modal>
      </div>
      <div
        class="w-full grid md:grid-cols-1 lg:grid-cols-2 xl:grid-cols-3 2xl:grid-cols-4 gap-4 vc-users-list"
        v-if="users"
      >
        <div class="vc-user" v-for="user of users">
          <div class="vc-dashboard-sidebar-body flex justify-between">
            <div class="flex">
              <div class="grid grid-cols-1 content-around vc-user-photo">
                <div class="vc-profile-photo-cover">
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    fill="currentColor"
                    class="bi bi-person-fill"
                    viewBox="0 0 16 16"
                  >
                    <path
                      d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6"
                    />
                  </svg>
                </div>
              </div>
              <div class="grid grid-cols-1 content-around vc-user-info">
                <h3>
                  <a href="#">
                    {{
                      user.firstName && user.lastName
                        ? sanitizeInput(user.firstName + ' ' + user.lastName)
                        : sanitizeInput(user.email)
                    }}
                  </a>
                </h3>
                <p>
                  {{
                    parseInt(user.role) === 0 ? t('administrator') : t('user')
                  }}
                </p>
              </div>
            </div>
            <div class="grid grid-cols-1 content-around"></div>
          </div>
          <div class="grid grid-cols-1 content-around vc-user-footer">
            <div class="flex justify-between">
              <div>
                <Icon
                  name="person_add_alt"
                  extraClass="vc-user-last-access-icon"
                />
                <span>
                  {{ calculateTime(user.created, time) }}
                </span>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="w-full vc-users-list" v-else>
        <div class="vc-no-users-found">
          <p>{{ $t('new_users_were_found') }}</p>
        </div>
      </div>
    </div>
    <Pagination
      v-if="total"
      :total="total"
      :page="page"
      :limit="24"
      @update:page="usersStore.setCurrentPage"
    />
  </div>
</template>
