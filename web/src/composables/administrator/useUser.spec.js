/*
 * @spec useUser
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-27
 *
 * This spec is used to test the user options updating
 */

// Installed Utils
import {
  describe,
  expect,
  test,
  vi
} from 'vitest';
import { useI18n } from 'vue-i18n';

// App Utils
import { useUser } from '@/composables/administrator/useUser';

// Create a mock for vue-i18n
vi.mock('vue-i18n', async () => {
    return {
        useI18n: vi.fn(),
    };
});

// Mock the useI18n method
const mockUseI18n = vi.mocked(useI18n);

// Test the useUser composable
describe('useUser', () => {

    test('it should update user information', async () => {

        mockUseI18n.mockReturnValue({
            t: (key) => key
        });

        const $fetch = vi.fn().mockResolvedValue({ success: true, message: 'Success' });
        vi.stubGlobal('$fetch', $fetch);

        const { update, successMessage, errorMessage } = useUser();
        await update({ name: 'John Doe', email: 'john@example.com' });
        expect(successMessage.value).toBe('Success');
        expect(errorMessage.value).toBe('');

    })

});