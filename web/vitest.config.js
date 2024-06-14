// Installed Utils
import { defineVitestConfig } from '@nuxt/test-utils/config';
import dotenv from 'dotenv';

// Enable env support
dotenv.config({ path: '.env' });

export default defineVitestConfig({
  test: {
    environment: 'nuxt',
    setupFiles: [
      './test/vitest.setup.ts',
    ],
    environmentOptions: {
      nuxt: {
        overrides: {
          runtimeConfig: {
            public: {
              siteName: process.env.SITE_NAME,
              siteUrl: process.env.SITE_URL,
              apiUrl: process.env.API_URL,
              env: 'test'
            },
          },
        },
      },
    },
  }
})
