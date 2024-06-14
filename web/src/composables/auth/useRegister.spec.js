/*
 * @spec useRegister
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-27
 *
 * This spec is used to test the users registration
 */

// Installed Utils
import {
    beforeAll,
    describe,
    expect,
    test,
    vi
  } from 'vitest';
import { useI18n } from 'vue-i18n';

// App Utils
import { useRegister } from '@/composables/auth/useRegister';

// Create a mock for vue-i18n
vi.mock('vue-i18n', () => {
    return {
        useI18n: vi.fn()
    }
});

// Mock the useI18n method
const mockUseI18n = vi.mocked(useI18n);

// Test the useRegister composable
describe('useRegister', () => {

    beforeAll(() => {
        mockUseI18n.mockReturnValue({
            t: (key) => key
        });
    });

    test('it should register successfully', async () => {

        const $fetch = vi.fn().mockResolvedValue({ success: true, message: 'Success' });
        vi.stubGlobal('$fetch', $fetch);
        const { register, successMessage, errorMessage, isLoading } = useRegister();
        await register({email: 'john@example.com', password: '1234'});
        expect(successMessage.value).toBe('Success');

    })

    test('it should fail registration', async () => {

        const $fetch = vi.fn().mockResolvedValue({ success: false, message: 'Failed' });
        vi.stubGlobal('$fetch', $fetch);
        const { register, successMessage, errorMessage, isLoading } = useRegister();
        await register({email: 'john@example.com', password: '1234'});
        expect(errorMessage.value).toBe('Failed');

    });

});