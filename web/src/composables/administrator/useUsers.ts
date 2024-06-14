// Installed Utils
import { useI18n } from 'vue-i18n';

// App Utils
import type ApiResponse from '~/interfaces/apiResponse';
import type { CreateUser } from '~/interfaces/user';

export const useUsers = () => {

  // Get i18n functions
  const { t } = useI18n();

  // Reactive reference for success messages
  const successMessage = ref('');

  // Reactive reference for error messages
  const errorMessage = ref('');

  /**
   * Create new user
   * 
   * @param user user's data
   */
  const createUser = async (user: CreateUser) => {
    // Reset the error and success message
    errorMessage.value = successMessage.value = '';

    // Get the runntime configuration
    const configuration = useRuntimeConfig();

    try {

      // Create a new user
      const response: ApiResponse<null> = await $fetch(
        configuration.public.apiUrl + 'api/v1.0/admin/users/create',
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(user),
        }
      );

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

  }

  return { createUser, successMessage, errorMessage }

}
