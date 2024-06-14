/*
 * @composable useUser
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-19
 *
 * This composable is used to update the current user settings
 */

// Installed Utils
import { useI18n } from 'vue-i18n';

// App Utils
import type ApiResponse from '~/interfaces/apiResponse';
import type { User } from '~/interfaces/user';

export const useUser = () => {
  // Get i18n functions
  const { t } = useI18n();

  // Reactive reference for success messages
  const successMessage = ref('');

  // Reactive reference for error messages
  const errorMessage = ref('');

  // Update the user information
  const update = async (options: User) => {
    // Reset the error and success message
    errorMessage.value = successMessage.value = '';

    // Get the runntime configuration
    const configuration = useRuntimeConfig();

    try {
      // Update the settings
      const response: ApiResponse<null> = await $fetch(
        configuration.public.apiUrl + 'api/v1.0/admin/users/options',
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(options),
        },
      );
      console.log(response);
      // Check if the response is success
      if (response.success) {
        // Set Message
        successMessage.value = sanitizeInput(response.message);
      } else if (!response.success) {
        // Set Message
        errorMessage.value = sanitizeInput(response.message);
      }
    } catch (error: unknown) {
      // Set error
      errorMessage.value =
        error instanceof Error ? error.message : t('an_error_has_occurred');
    }
  };

  return { successMessage, errorMessage, update };
};
