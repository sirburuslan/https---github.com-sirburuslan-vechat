// System Utils
import { config } from '@vue/test-utils';

// Installed utils
import { createI18n } from 'vue-i18n';

// App Utils
import i18nConfig from '../i18n.config';

// Register the directives
config.global.directives = {
  'page-load': {
    mounted(el, binding) {},
  },
  'scroll-page': {
    mounted(el, binding) {}
  }
}

// Create a stub for NuxtLink
const NuxtLinkStub = {
  template: '<a><slot /></a>'
}

// Register the stubs
config.global.stubs = {
  NuxtLink: NuxtLinkStub
}

// Register the I18n plugin
config.global.plugins.push(createI18n(i18nConfig));