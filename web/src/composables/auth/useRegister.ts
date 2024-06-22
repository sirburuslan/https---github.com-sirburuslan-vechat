/*
 * @composable useRegister
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-30
 *
 * This composable is used for the user registration
 */

// Installed Utils
import { useI18n } from 'vue-i18n';

// App Utils
import type { User } from '~/interfaces/user';
import type ApiResponse from '~/interfaces/apiResponse';
import { sanitizeInput } from '~/utils/sanitize';

// Register the users
export const useRegister = () => {
  // Get i18n functions
  const { t } = useI18n();

  // Reactive reference for success messages
  const successMessage = ref('');

  // Reactive reference for error message
  const errorMessage = ref('');

  // Loading marker
  const isLoading = ref<boolean>(false);

  // Register the user
  const register = async (user: User) => {
    // Enable the loading
    isLoading.value = true;

    // Reset the error and success message
    successMessage.value = errorMessage.value = '';

    // Get the runntime configuration
    const configuration = useRuntimeConfig();

    try {
      // Register the user
      const response: ApiResponse<null> = await $fetch(
        configuration.public.apiUrl + 'api/v1.0/auth/registration',
        {
          method: 'POST',
          body: JSON.stringify(user),
          headers: {
            'Content-Type': 'application/json',
          },
        },
      );

      // Disable the loading
      isLoading.value = false;

      // Check if the message is success
      if (response.success) {
        // Set Message
        successMessage.value = sanitizeInput(response.message);
        // Set a pause
        setTimeout(async () => {
          await navigateTo('/auth/signin');
        }, 2000);
      } else {
        // Set Message
        errorMessage.value = sanitizeInput(response.message);
      }
    } catch (error: unknown) {
      // Disable the loading
      isLoading.value = false;

      // Set error
      errorMessage.value =
        error instanceof Error ? error.message : t('an_error_has_occurred');
    }
  };

  return { register, successMessage, errorMessage, isLoading };
};
