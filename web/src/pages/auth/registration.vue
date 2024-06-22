<script lang="ts" setup>
// System Utils
import { ref } from 'vue';

// Installed Utils
import { useI18n } from 'vue-i18n';
import { useVuelidate } from '@vuelidate/core';
import { required, email, minLength, maxLength, helpers } from '@vuelidate/validators';

// Get the registration composable
const { register, successMessage, errorMessage, isLoading } = useRegister();

// Get i18n functions
const { t } = useI18n();

// Define page name
useHead({
  title: t('sign_up'),
});

// Get runtime configuration
const config = useRuntimeConfig();

// Get the site's name
const siteName = config.public.siteName ?? '';

// Create a reactive value for password input
const passwordInput = ref('password');

// Create a handler for show password button
const showPassword = () => {
  // Change the password input type
  passwordInput.value =
    passwordInput.value === 'password' ? 'text' : 'password';
};

// Form Inputs
const state = reactive({
  email: '',
  password: '',
});

// Form Error Messages
const message = reactive({
  emailError: '',
  passwordError: '',
});

// Set Rules for validation
const rules = computed(() => ({
  email: {
    required: helpers.withMessage(t('auth_email_short'), required),
    email: helpers.withMessage(t('auth_email_not_valid'), email)
  },
  password: {
    required: helpers.withMessage(t('auth_password_short'), required),
    minLength: helpers.withMessage(t('auth_password_short'), minLength(8)),
    maxLength: helpers.withMessage(t('auth_password_long'), maxLength(20))
  },
}));

// Combine valdation rules with inputs value
const v$ = useVuelidate(rules, state);

// Handle the form submit
const handleSubmit = async () => {
  // Reset error messages
  message.emailError = '';
  message.passwordError = '';

  // Trigger validation
  v$.value.$touch();

  // Verify if errors exists
  if (v$.value.$invalid) {
    // List the errors
    v$.value.$silentErrors.map((error) => {
      // Check if property is email
      if (error.$property == 'email') {
        message.emailError = error.$message;
      } else {
        message.passwordError = error.$message;
      }
    });

    return;
  }

  // Register the user
  await register(state);
};
</script>

<template>
  <form
    @submit.prevent="handleSubmit"
    class="px-8 pt-6 pb-6 mb-4 vc-auth-main-form"
  >
    <div class="mt-4 mb-14">
      <h3 class="text-center">{{ $t('sign_up_to') }} {{ siteName }}</h3>
    </div>
    <div class="mb-6 relative">
      <input
        name="vc-auth-main-form-email"
        type="text"
        placeholder=" "
        autoComplete="current-email"
        v-model="state.email"
        id="vc-auth-main-form-email"
        class="block px-2.5 pb-2.5 pt-4 peer vc-auth-main-form-input"
      />
      <label
        for="vc-auth-main-form-email"
        class="absolute duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] px-2 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1"
      >
        {{ $t('enter_your_email_address') }}
      </label>
      <div class="vc-auth-main-form-input-error-message">
        {{ message.emailError }}
      </div>
    </div>
    <div class="mb-6 relative">
      <input
        name="vc-auth-main-form-password"
        :type="passwordInput"
        placeholder=" "
        autoComplete="current-password"
        v-model="state.password"
        id="vc-auth-main-form-password"
        class="block px-2.5 pb-2.5 pt-4 peer vc-auth-main-form-input"
      />
      <label
        for="vc-auth-main-form-password"
        class="absolute duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] px-2 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1"
      >
        {{ $t('enter_your_password') }}
      </label>
      <button
        type="button"
        class="vc-auth-main-form-show-password-btn"
        @click="showPassword"
      >
        <Icon name="visibility" extraClass="vc-auth-main-form-eye-icon" />
        <Icon
          name="visibility_off"
          extraClass="vc-auth-main-form-eye-hide-icon"
        />
      </button>
      <div class="vc-auth-main-form-input-error-message">
        {{ message.passwordError }}
      </div>
    </div>
    <div class="mt-4 mb-3">
      <button
        type="submit"
        class="py-2 px-4 focus:outline-none focus:shadow-outline vc-auth-main-form-submit-btn"
        :class="{ 'vc-auth-main-form-submit-active-btn': isLoading }"
      >
        {{ $t('sign_up') }}
        <Icon name="arrow_forward" extraClass="vc-auth-main-form-submit-icon" />
        <Icon
          name="autorenew"
          extraClass="vc-rotate-animation vc-auth-main-form-submitting-icon"
        />
      </button>
    </div>
    <div class="vc-auth-main-form-alerts">
      <div
        class="flex items-center px-4 py-3 mb-4 vc-auth-main-form-alert-success"
        role="alert"
        v-if="successMessage"
      >
        <Icon
          name="notifications"
          extraClass="vc-auth-main-form-alert-success-icon"
        />
        <p>{{ successMessage }}</p>
      </div>
      <div
        class="flex items-center px-4 py-3 vc-auth-main-form-alert-error"
        role="alert"
        v-else-if="errorMessage"
      >
        <Icon
          name="notifications"
          extraClass="vc-auth-main-form-alert-error-icon"
        />
        <p>{{ errorMessage }}</p>
      </div>
    </div>
    <div class="vc-auth-additional-link">
      <p>
        {{ $t('do_you_have_account') }}
        <NuxtLink
          to="/auth/signin"
          class="inline-block vc-auth-main-form-reset-link"
          :aria-label="$t('sign_in')"
        >
          {{ $t('sign_in') }}
        </NuxtLink>
      </p>
    </div>
  </form>
</template>
