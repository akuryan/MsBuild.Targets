# NewRelic.Synthetics.Api
MsBuild targets for working with NewRelic Synthetics API (disable monitor before deployment and enable it after)

#NewRelic.Synthetics.Api.Tests
Tests is dumb - add your api key to app.config and observe, that api requests to real new relic endpoints are working

`src/NewRelic/NewRelic.Synthetics.Api.Tests/App.config` is ignored by command `git update-index --skip-worktree src/NewRelic/NewRelic.Synthetics.Api.Tests/App.config`, so, all changes there will be untracked.
If you feel urgent requirement to add something there - this ignore can be reverted by command `git update-index --no-skip-worktree src/NewRelic/NewRelic.Synthetics.Api.Tests/App.config`

You can view all skips by command `git ls-files -v . | grep ^S`
