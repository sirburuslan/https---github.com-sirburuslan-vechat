/*
 * @composable useLogin
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-05
 *
 * This composable is used for the user login
 */

// Installed Utils
import { useI18n } from 'vue-i18n';

// App Utils
import type { User } from '~/interfaces/user';
import type ApiResponse from '~/interfaces/apiResponse';
import { sanitizeInput } from '~/utils/sanitize';
import { useUserStore } from '~/stores/user';

// Login the users
export const useLogin = () => {
  // Get i18n functions
  const { t } = useI18n();

  // Reactive reference for success messages
  const successMessage = ref('');

  // Reactive reference for error message
  const errorMessage = ref('');

  // Loading marker
  const isLoading = ref<boolean>(false);

  // Login the user
  const login = async (user: User) => {
    // Enable the loading
    isLoading.value = true;

    // Reset the error and success message
    successMessage.value = errorMessage.value = '';

    // Get the runntime configuration
    const configuration = useRuntimeConfig();

    // Get the user store
    const userStore = useUserStore();

    try {
      // Try to login the user
      const response: ApiResponse<User> = await $fetch(
        configuration.public.apiUrl + 'api/v1.0/auth/signin',
        {
          method: 'POST',
          body: user,
        },
      );

      // Check if response is successfully
      if (response.success && response.content) {
        userStore.saveUser(response.content);
        successMessage.value = sanitizeInput(response.message);
      } else {
        errorMessage.value = sanitizeInput(response.message);
      }

      // Disable the loading
      isLoading.value = false;
    } catch (error: unknown) {
      errorMessage.value =
        error instanceof Error ? error.message : t('an_error_has_occurred');
      isLoading.value = false;
    }
  };

  return { login, successMessage, errorMessage, isLoading };
};
