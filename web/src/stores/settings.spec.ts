// Installed Utils
import { createPinia, setActivePinia } from 'pinia';
import {
    describe,
    vi,
    test,
    expect,
    beforeEach
} from 'vitest';

describe('Settings Store', () => {
  const settingsStore = useSettingsStore();
  const userStore = useUserStore();

  beforeEach(() => {
    setActivePinia(createPinia());
    settingsStore.$reset();
    userStore.$reset();
  });

  test('should fetch website and user options and update two stores', async () => {
    const $fetch = vi.fn().mockResolvedValue({
      success: true,
      website: { sidebar: 1 },
      user: { firstName: 'John' },
    });
    vi.stubGlobal('$fetch', $fetch);
    await settingsStore.getOptions('/');
    expect(settingsStore.options).toEqual({ sidebar: 1 });
    expect(userStore.user?.firstName).toEqual('John');
  });

  test('should follow the correct request and failed response', async () => {
    const fetchMock = vi.spyOn(global, '$fetch').mockResolvedValueOnce({
      success: false,
      message: 'Failed',
    });

    const consoleErrorMock = vi.spyOn(console, 'error').mockImplementation(() => {});

    try {
      await settingsStore.getOptions('/');
    } catch (error) {
      expect(error).toBeDefined();
    }
    
    expect(fetchMock).toHaveBeenCalledWith(
      process.env.API_URL + 'api/v1.0/settings',
      {},
    );

    fetchMock.mockRestore();
    consoleErrorMock.mockRestore();
  });
});