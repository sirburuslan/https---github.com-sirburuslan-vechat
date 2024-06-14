import eslint from '@eslint/js'
import tseslint from 'typescript-eslint'
import vueParser from 'vue-eslint-parser'

export default tseslint.config(
  eslint.configs.recommended,
  ...tseslint.configs.recommended,
  ...tseslint.configs.recommendedTypeChecked,
  {
    languageOptions: {
      parser: vueParser,
      parserOptions: {
        parser: tseslint.parser,
        sourceType: 'module',
        project: true,
        exclude: ['node_modules', 'build', 'public', '.nuxt'],
      },
    },
  },
  {
    files: ['*.js'],
    ...tseslint.configs.disableTypeChecked,
  },
)
