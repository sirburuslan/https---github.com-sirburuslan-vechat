// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  app: {
    head: {
      charset: 'utf-8',
      viewport: 'width=device-width, initial-scale=1',
    }
  },
  runtimeConfig: {
    public: {
      siteName: process.env.SITE_NAME,
      siteUrl: process.env.SITE_URL,
      apiUrl: process.env.API_URL,
      env: 'development'
    }
  },
  srcDir: 'src',
  css: ['@/assets/main.css'],
  routeRules: {
    // Homepage pre-rendered at build time
    //'/': { swr: 1 },
  },
  devtools: { enabled: true },
  modules: [
    '@nuxtjs/tailwindcss',
    '@nuxtjs/i18n',
    '@nuxt/content',
    '@nuxt/image',
    '@nuxt/test-utils/module',
    '@pinia/nuxt'
  ],
  components: [
    '~/components',
    '~/components/pages/home',
    '~/components/ui'
  ],
  plugins: [
    '~/plugins/scrollPage.plugin.ts',
    '~/plugins/pageLoad.plugin.ts',
    '~/plugins/showNotification.plugin.ts',
    '~/plugins/cookie.plugin.ts'
  ],
  imports: {
    autoImport: true,
    dirs: [
      'composables/**',
      'stores/**'
    ]
  },
})
